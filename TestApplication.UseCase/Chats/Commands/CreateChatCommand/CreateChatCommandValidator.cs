using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.Commands.CreateChatCommand
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            RuleFor(c => c.CreatorId)
                .GreaterThan(0).WithMessage("ChatMessageId is less than 1!");
            RuleFor(c => c.InvitedUsersIds)
                .Must(ids => ids.Length > 0).WithMessage("At least should be 1 invited user!")
                .ForEach(i => i.GreaterThan(0).WithMessage("InvitedUsersIds id is less than 1!"));
        }
    }
}
