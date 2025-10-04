using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Channels.DTO;
using TestApplication.UseCase.Channels.QueryReps;

namespace TestApplication.UseCase.Channels.Queries.GetAllMessagesByChannelQuery
{
    public class GetAllMessagesByChannelQuery : IQuery<ChannelInfoResponse>
    {
        public int ChannelId { get; set; }
    }
    public class GetAllMessagesByChannelQueryHandler(
        IChannelQueryRep rep)
        : IQueryHandler<GetAllMessagesByChannelQuery, ChannelInfoResponse>
    {
        public async Task<Result<ChannelInfoResponse>> Handle(GetAllMessagesByChannelQuery query, CancellationToken token = default)
        {
            return Result<ChannelInfoResponse>.Success(await rep.GetAllMessageByChannelId(query.ChannelId, token));
        }
    }
}
