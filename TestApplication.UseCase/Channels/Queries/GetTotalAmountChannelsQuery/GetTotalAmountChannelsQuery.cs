using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Channels.QueryReps;

namespace TestApplication.UseCase.Channels.Queries.GetTotalAmountChannelsQuery
{
    public class GetTotalAmountChannelsQuery : IQuery<int>
    {
    }
    public class GetTotalAmountChannelsQueryHandler(IChannelQueryRep rep)
        : IQueryHandler<GetTotalAmountChannelsQuery, int>
    {
        public async Task<Result<int>> Handle(GetTotalAmountChannelsQuery query, CancellationToken token = default)
        {
            return Result<int>.Success(await rep.GetTotalAmountChannels(token));
        }
    }
}
