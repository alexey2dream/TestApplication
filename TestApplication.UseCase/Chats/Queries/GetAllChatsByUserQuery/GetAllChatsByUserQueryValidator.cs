using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.UseCase.Chats.Queries.GetUserAllChatsQuery;

namespace TestApplication.UseCase.Chats.Queries.GetAllChatsByUserIdQuery
{
    public class GetAllChatsByUserQueryValidator : AbstractValidator<GetAllChatsByUserQuery>
    {
        public GetAllChatsByUserQueryValidator()
        {
            RuleFor(c => c.PageNum)
                .GreaterThan(0).WithMessage("PageNum is less than 1!");
            RuleFor(c => c.PageSize)
                .GreaterThan(0).WithMessage("PageSize is less than 1!");
            RuleFor(c => c.UserId)
                .GreaterThan(0).WithMessage("UserId is less than 1!");
        }
    }
}
