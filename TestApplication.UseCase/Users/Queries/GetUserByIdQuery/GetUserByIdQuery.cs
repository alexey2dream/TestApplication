using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Users.DTO;
using TestApplication.UseCase.Users.QueryReps;

namespace TestApplication.UseCase.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery : IQuery<UserResponse>
    {
        public int Id { get; set; }
    }
    public class GetUserByIdQueryHandler(
        IUserQueryRep rep)
        : IQueryHandler<GetUserByIdQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken token = default)
        {
            return Result<UserResponse>.Success(await rep.GetById(query.Id, token));
        }
    }

}
