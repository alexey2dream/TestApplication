using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.Commands.CreateChatMessageCommand
{
    public class CreateChatMessageCommandValidator : AbstractValidator<CreateChatMessageCommand>
    {
        public CreateChatMessageCommandValidator()
        {
            RuleFor(c => c.ChatId)
                    .GreaterThan(0).WithMessage("ChatId is less than 1!");
            RuleFor(c => c.SenderId)
                    .GreaterThan(0).WithMessage("SenderId is less than 1!");
            RuleFor(c => c.Text)
                .NotNull().WithMessage("Text is null!")
                .NotEmpty().WithMessage("Text is empty!");
        }
    }
}
