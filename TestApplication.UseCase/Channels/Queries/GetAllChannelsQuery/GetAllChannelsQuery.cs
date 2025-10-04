using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Channels.DTO;
using TestApplication.UseCase.Channels.QueryReps;
using TestApplication.UseCase.Chats.DTO;

namespace TestApplication.UseCase.Channels.Queries.GetAllChannelsQuery
{
    public class GetAllChannelsQuery : IQuery<List<ChannelResponse>>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllChannelsQueryHandler(
        IChannelQueryRep rep)
        : IQueryHandler<GetAllChannelsQuery, List<ChannelResponse>>
    {
        public async Task<Result<List<ChannelResponse>>> Handle(GetAllChannelsQuery query, CancellationToken token = default)
        {
            return Result<List<ChannelResponse>>.Success(
                await rep.GetAllByUserId(query.PageNum, query.PageSize, token));
        }
    }
}
