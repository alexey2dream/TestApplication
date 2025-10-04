using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.DTO;
using TestApplication.UseCase.Chats.QueryReps;

namespace TestApplication.UseCase.Chats.Queries.GetUserAllChatsQuery
{
    public class GetAllChatsByUserQuery : IQuery<List<ChatResponse>>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int UserId { get; set; }
    }
    public class GetAllChatsByUserIdQueryHandler(
        IChatQueryRep rep)
        : IQueryHandler<GetAllChatsByUserQuery, List<ChatResponse>>
    {
        public async Task<Result<List<ChatResponse>>> Handle(GetAllChatsByUserQuery query, CancellationToken token = default)
        {
            return Result<List<ChatResponse>>.Success(
                await rep.GetAllByUserId(query.UserId, query.PageNum, query.PageSize, token));
        }
    }
}
