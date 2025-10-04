using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.Commands.CreateChannelCommand
{
    public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
    {
        public CreateChannelCommandValidator()
        {
            RuleFor(c => c.CreatorId)
                    .GreaterThan(0).WithMessage("CreatorId is less than 1!");
            RuleFor(c => c.Title)
                .NotNull().WithMessage("Title is null!")
                .NotEmpty().WithMessage("Title is empty!");
        }
    }
    
    
}
