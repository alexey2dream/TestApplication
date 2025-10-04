using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Domain.Channels.Services;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Channels.Commands.CreateChannelCommand
{
    public class CreateChannelCommand : ICommand
    {
        public string Title { get; set; }
        public int CreatorId { get; set; }
    }
    public class CreateChannelCommandHandler(
        IUserRep userRep,
        IChannelRep rep,
        ChannelService service)
        : ICommandHandler<CreateChannelCommand>
    {
        public async Task<Result> Handle(CreateChannelCommand command, CancellationToken token = default)
        {
            var creator = await userRep.GetById(command.CreatorId, token);
            if (creator == null)
                return Result.Failure("User not exists!");

            var result = await service.Create(command.Title, creator, token);
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
