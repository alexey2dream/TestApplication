using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Domain.Chats.Services;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Models;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Chats.Commands.CreateChatCommand
{
    public class CreateChatCommand : ICommand
    {
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public int[] InvitedUsersIds { get; set; }
    }
    public class CreateChatCommandHandler(
        IUserRep userRep,
        IChatRep rep,
        ChatService service)
        : ICommandHandler<CreateChatCommand>
    {
        public async Task<Result> Handle(CreateChatCommand command, CancellationToken token = default)
        {
            var creator = await userRep.GetById(command.CreatorId, token);
            if (creator == null)
                return Result.Failure("User not exists!");
            List<User> participants = new List<User>();
            foreach(var u in command.InvitedUsersIds)
            {
                var participant = await userRep.GetById(u, token);
                if (participant == null)
                    return Result.Failure($"Participant with id#{u} not exists!");
                participants.Add(participant);
            }

            var result = service.Create(command.Title, creator, participants);
            if (!result.IsSuccess)
                return result;

            if (!await rep.Add(result.Value, token))
                return Result.Failure("Invalid add!");
            if (rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
