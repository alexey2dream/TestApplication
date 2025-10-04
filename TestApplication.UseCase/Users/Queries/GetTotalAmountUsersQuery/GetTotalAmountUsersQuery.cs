using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Users.QueryReps;

namespace TestApplication.UseCase.Users.Queries.GetTotalAmountUsersQuery
{
    public class GetTotalAmountUsersQuery : IQuery<int>
    {
    }
    public class GetTotalAmountUsersQueryHandler(IUserQueryRep rep)
        : IQueryHandler<GetTotalAmountUsersQuery, int>
    {
        public async Task<Result<int>> Handle(GetTotalAmountUsersQuery query, CancellationToken token = default)
        {
            return Result<int>.Success(await rep.GetTotalAmountUsers());
        }
    }
}
