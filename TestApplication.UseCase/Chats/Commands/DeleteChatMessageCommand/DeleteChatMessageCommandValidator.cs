using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.Commands.DeleteChatMessageCommand
{
    public class DeleteChatMessageCommandValidator
        : AbstractValidator<DeleteChatMessageCommand>
    {
        public DeleteChatMessageCommandValidator()
        {
            RuleFor(c => c.ChatMessageId)
                .GreaterThan(0).WithMessage("ChatMessageId is less than 1!");
        }
    }
}
