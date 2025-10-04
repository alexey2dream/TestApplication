using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.Commands.UpdateChatTitleCommand
{
    public class UpdateChatTitleCommandValidator : AbstractValidator<UpdateChatTitleCommand>
    {
        public UpdateChatTitleCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id is less than 1!");
            RuleFor(c => c.NewTitle)
                .NotNull().WithMessage("NewTitle is null!")
                .NotEmpty().WithMessage("NewTitle is empty!");
        }
    }
}
