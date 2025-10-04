using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Users.Commands.BanUserCommand
{
    public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
    {
        public BanUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id is less than 1!");
        }
    }
}
