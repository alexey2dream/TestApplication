using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Domain.Chats.Services;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Chats.Commands.DeleteChatCommand
{
    public class DeleteChatCommand : ICommand
    {
        public int CreatorId { get; set; }
        public int ChatId { get; set; }
    }
    public class DeleteChatCommandHandler(
        IChatRep rep,
        IUserRep userRep,
        ChatService service)
        : ICommandHandler<DeleteChatCommand>
    {
        public async Task<Result> Handle(DeleteChatCommand command, CancellationToken token = default)
        {
            var user = await userRep.GetById(command.CreatorId, token);
            if (user == null)
                return Result.Failure("User not exists!");
            var chat = await rep.GetById(command.ChatId, token);
            if (chat == null)
                return Result.Failure("Chat not exists!");

            var result = service.Delete(user, chat);
            if (!result.IsSuccess)
                return result;
            if(rep.Save() == 0)
                return Result.Failure("Invalid save!");

            return Result.Success();
        }
    }
}
