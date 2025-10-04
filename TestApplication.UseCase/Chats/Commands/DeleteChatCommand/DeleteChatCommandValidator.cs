using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.Commands.DeleteChatCommand
{
    public class DeleteChatCommandValidator : AbstractValidator<DeleteChatCommand>
    {
        public DeleteChatCommandValidator()
        {
            RuleFor(c => c.CreatorId)
                .GreaterThan(0).WithMessage("CreatorId is less than 1!");
            RuleFor(c => c.ChatId)
                .GreaterThan(0).WithMessage("ChatId is less than 1!");
        }
    }
}
