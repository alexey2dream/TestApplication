using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Users.DTO;
using TestApplication.UseCase.Users.QueryReps;

namespace TestApplication.UseCase.Users.Queries.GetAllUsersQuery
{
    public class GetAllUsersQuery : IQuery<List<UserResponse>>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllUsersQueryHandler(
        IUserQueryRep rep)
        : IQueryHandler<GetAllUsersQuery, List<UserResponse>>
    {
        public async Task<Result<List<UserResponse>>> Handle(GetAllUsersQuery query, CancellationToken token = default)
        {
            return Result<List<UserResponse>>.Success(await rep.GetAll(query.PageNum, query.PageSize, token));
        }
    }
}
