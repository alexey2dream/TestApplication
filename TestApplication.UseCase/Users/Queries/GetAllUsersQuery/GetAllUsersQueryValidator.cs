using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Users.Queries.GetAllUsersQuery
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            RuleFor(c => c.PageNum)
                .GreaterThan(0).WithMessage("PageNum is less than 1!");
            RuleFor(c => c.PageSize)
                .GreaterThan(0).WithMessage("PageSize is less than 1!");
        }
    }
}
