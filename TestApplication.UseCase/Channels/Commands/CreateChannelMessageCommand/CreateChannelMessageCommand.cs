using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Domain.Channels.Services;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Channels.Commands.CreateChannelMessageCommand
{
    public class CreateChannelMessageCommand : ICommand
    {
        public int ChannelId { get; set; }
        public string Text { get; set; }
    }
    public class CreateChannelMessageCommandHandler(
        IChannelRep rep,
        ChannelService service)
        : ICommandHandler<CreateChannelMessageCommand>
    {
        public async Task<Result> Handle(CreateChannelMessageCommand command, CancellationToken token = default)
        {
            var channel = await rep.GetById(command.ChannelId, token);
            if (channel == null)
                return Result.Failure("Channel not exists!");

            var result = service.CreateMessage(command.Text, channel);
            if (!result.IsSuccess)
                return result;

            if (rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
