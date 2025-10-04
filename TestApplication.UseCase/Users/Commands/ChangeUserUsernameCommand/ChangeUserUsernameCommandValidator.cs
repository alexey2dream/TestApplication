using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Users.Commands.ChangeUserUsernameCommand
{
    public class ChangeUserUsernameCommandValidator : AbstractValidator<ChangeUserUsernameCommand>
    {
        public ChangeUserUsernameCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id is less than 1!");
            RuleFor(c => c.NewUsername)
                .NotNull().WithMessage("NewUsername is null!")
                .NotEmpty().WithMessage("NewUsername is empty!");
        }
    }
}
