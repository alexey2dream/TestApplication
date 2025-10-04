using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Domain.Channels.Services;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Channels.Commands.DeleteChannelCommand
{
    public class DeleteChannelCommand : ICommand
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteChannelCommandHandler(
        IChannelRep rep,
        IUserRep userRep,
        ChannelService service)
        : ICommandHandler<DeleteChannelCommand>
    {
        public async Task<Result> Handle(DeleteChannelCommand command, CancellationToken token = default)
        {
            var user = await userRep.GetById(command.UserId, token);
            if (user == null)
                return Result.Failure("User not exists!");
            var channel = await rep.GetById(command.Id, token);
            if (channel == null)
                return Result.Failure("Chat not exists!");
            var result = service.Delete(user, channel);
            if (!result.IsSuccess)
                return result;
            if (rep.Save() == 0)
                return Result.Failure("Invalid save!");

            return Result.Success();
        }
    }
}
