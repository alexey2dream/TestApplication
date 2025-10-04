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

namespace TestApplication.UseCase.Chats.Commands.DeleteChatMessageCommand
{
    public class DeleteChatMessageCommand : ICommand
    {
        public int ChatMessageId { get; set; }
    }
    public class DeleteChatMessageCommandHandler(
        IChatMessageRep rep,
        IChatRep chatRep,
        ChatService chatService)
        : ICommandHandler<DeleteChatMessageCommand>
    {
        public async Task<Result> Handle(DeleteChatMessageCommand command, CancellationToken token = default)
        {
            var message = await rep.GetById(command.ChatMessageId, token);
            if (message == null)
                return Result.Failure("Message not exists!");
            var chat = await chatRep.GetById(message.ChatId, token);
            if (chat == null)
                return Result.Failure("Chat not exists!");
            var result = chatService.DeleteMessage(chat, message);
            if (!result.IsSuccess)
                return result;
            if (chatRep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
