using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Chats.DTO;
using TestApplication.UseCase.Chats.QueryReps;

namespace TestApplication.UseCase.Chats.Queries.GetAllMessagesByChatQuery
{
    public class GetChatWithAllMessagesQuery : IQuery<ChatInfoResponse>
    {
        public int ChatId { get; set; }
    }
    public class GetChatWithAllMessagesQueryHandler
        (IChatQueryRep rep)
        : IQueryHandler<GetChatWithAllMessagesQuery, ChatInfoResponse>
    {
        public async Task<Result<ChatInfoResponse>> Handle(GetChatWithAllMessagesQuery query, CancellationToken token = default)
        {
            return Result<ChatInfoResponse>.Success(await rep.GetByIdWithAllPages(query.ChatId, token));
        }
    }
}
