using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Channels.QueryReps;

namespace TestApplication.UseCase.Channels.Queries.GetTotalAmountMessagesByChannel
{
    public class GetTotalAmountMessagesByChannel : IQuery<int>
    {
        public int ChannelId { get; set; }
    }
    public class GetTotalAmountMessagesByChannelHandler(IChannelQueryRep rep)
        : IQueryHandler<GetTotalAmountMessagesByChannel, int>
    {
        public async Task<Result<int>> Handle(GetTotalAmountMessagesByChannel query, CancellationToken token = default)
        {
            return Result<int>.Success(await rep.GetTotalAmountMessagesByChannelId(query.ChannelId, token));
        }
    }
}
