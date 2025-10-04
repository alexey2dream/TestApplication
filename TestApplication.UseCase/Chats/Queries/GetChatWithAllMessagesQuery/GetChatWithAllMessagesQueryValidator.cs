using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.UseCase.Chats.Queries.GetAllMessagesByChatQuery;

namespace TestApplication.UseCase.Chats.Queries.GetChatWithAllMessagesQuery1
{
    public class GetChatWithAllMessagesQueryValidator : AbstractValidator<GetChatWithAllMessagesQuery>
    {
        public GetChatWithAllMessagesQueryValidator()
        {
            RuleFor(c => c.ChatId)
                .GreaterThan(0).WithMessage("ChatId is less than 1!");
        }
    }
}
