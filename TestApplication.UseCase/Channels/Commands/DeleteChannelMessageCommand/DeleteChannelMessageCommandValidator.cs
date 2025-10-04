using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Channels.Commands.DeleteChannelMessageCommand
{
    public class DeleteChannelMessageCommandValidator : AbstractValidator<DeleteChannelMessageCommand>
    {
        public DeleteChannelMessageCommandValidator()
        {
            RuleFor(c => c.ChannelMessageId)
                .GreaterThan(0).WithMessage("ChannelMessageId is less than 1!");
        }
    }
}
