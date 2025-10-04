using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.Commands.CreateChannelMessageCommand
{
    public class CreateChannelMessageCommandValidator : AbstractValidator<CreateChannelMessageCommand>
    {
        public CreateChannelMessageCommandValidator()
        {
            RuleFor(c => c.ChannelId)
                    .GreaterThan(0).WithMessage("ChannelId is less than 1!");
            RuleFor(c => c.Text)
                .NotNull().WithMessage("Text is null!")
                .NotEmpty().WithMessage("Text is empty!");
        }
    }
}
