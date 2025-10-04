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

namespace TestApplication.UseCase.Chats.Commands.CreateChatMessageCommand
{
    public class CreateChatMessageCommand : ICommand
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; }
    }
    public class CreateChatMessageCommandHandler(
        IUserRep userRep,
        IChatRep chatRep,
        ChatService service)
        : ICommandHandler<CreateChatMessageCommand>
    {
        public async Task<Result> Handle(CreateChatMessageCommand command, CancellationToken token = default)
        {
            var sender = await userRep.GetById(command.SenderId, token);
            if (sender == null)
                return Result.Failure("Sender not exists!");
            var chat = await chatRep.GetById(command.ChatId, token);
            if (chat == null)
                return Result.Failure("Chat not exists!");
            
            var result = service.CreateMessage(command.Text, sender, chat);
            if (!result.IsSuccess)
                return result;

            if (chatRep.Save() == 0)
                return Result.Failure("Invalid save!");
            return Result.Success();
        }
    }
}
