using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Domain.Channels.Services;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Channels.Commands.ChangeTitleChannelCommand
{
    public class ChangeTitleChannelCommand : ICommand
    {
        public int Id { get; set; }
        public string NewTitle { get; set; }
    }
    public class ChangeTitleChannelCommandHandler(
        IChannelRep rep,
        ChannelService service)
        : ICommandHandler<ChangeTitleChannelCommand>
    {
        public async Task<Result> Handle(ChangeTitleChannelCommand command, CancellationToken token = default)
        {
            var channel = await rep.GetById(command.Id, token);
            if (channel == null)
                return Result.Failure("Channel not exists!");

            var result = await service.ChangeTitle(command.NewTitle, channel, token);
            if (!result.IsSuccess)
                return result;

            if(rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
