using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Domain.Channels.Services;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Domain.Chats.Services;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Repositories;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Channels.Commands.DeleteChannelMessageCommand
{
    public class DeleteChannelMessageCommand : ICommand
    {
        public int ChannelMessageId { get; set; }
    }
    public class DeleteChannelMessageCommandHandler(
        IChannelRep rep,
        IChannelMessageRep messageRep,
        ChannelService service)
        : ICommandHandler<DeleteChannelMessageCommand>
    {
        public async Task<Result> Handle(DeleteChannelMessageCommand command, CancellationToken token = default)
        {
            var message = await messageRep.GetById(command.ChannelMessageId, token);
            if (message == null)
                return Result.Failure("Message not exists!");
            var channel = await rep.GetById(message.ChannelId, token);
            if (channel == null)
                return Result.Failure("Channel not exists!");
            var result = service.DeleteMessage(channel, message);
            if (!result.IsSuccess)
                return result;
            if (rep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
