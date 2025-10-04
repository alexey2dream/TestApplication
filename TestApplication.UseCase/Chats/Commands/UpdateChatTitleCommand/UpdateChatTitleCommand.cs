using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Domain.Chats.Services;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;

namespace TestApplication.UseCase.Chats.Commands.UpdateChatTitleCommand
{
    public class UpdateChatTitleCommand : ICommand
    {
        public int Id { get; set; }
        public string NewTitle { get; set; }
    }
    public class UpdateChatTitleCommandHandler(
        IChatRep rep,
        ChatService service)
        : ICommandHandler<UpdateChatTitleCommand>
    {
        public async Task<Result> Handle(UpdateChatTitleCommand command, CancellationToken token = default)
        {
            var chat = await rep.GetById(command.Id, token);
            if (chat == null)
                return Result.Failure("Chat not exists!");

            var result = service.UpdateTitle(chat, command.NewTitle);
            if (!result.IsSuccess)
                return result;
            
            if(rep.Save() == 0)
                return Result.Failure("Chat not exists!");
            return Result.Success();    
        }
    }
}
