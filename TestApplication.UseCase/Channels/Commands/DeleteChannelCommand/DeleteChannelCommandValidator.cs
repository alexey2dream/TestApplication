using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.Commands.DeleteChannelCommand
{
    public class DeleteChannelCommandValidator : AbstractValidator<DeleteChannelCommand>
    {
        public DeleteChannelCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id is less than 1!");
            RuleFor(c => c.UserId)
                .GreaterThan(0).WithMessage("UserId is less than 1!");
        }
    }
}
