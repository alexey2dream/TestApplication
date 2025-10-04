using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Users.Commands.CreateUserCommand
{
    public class CreateUserCommandValidator 
        : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Username)
                .NotNull().WithMessage("Username is null!")
                .NotEmpty().WithMessage("Username is empty!");
        }
    }
}
