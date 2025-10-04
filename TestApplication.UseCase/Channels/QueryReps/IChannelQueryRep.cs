using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.UseCase.Channels.DTO;
using TestApplication.UseCase.Chats.DTO;

namespace TestApplication.UseCase.Channels.QueryReps
{
    public interface IChannelQueryRep
    {
        Task<List<ChannelResponse>> GetAllByUserId(int pageNum, int pageSize, CancellationToken token);
        Task<ChannelInfoResponse> GetAllMessageByChannelId(int channelId, CancellationToken token);
        Task<int> GetTotalAmountChannels(CancellationToken token);
        Task<int> GetTotalAmountMessagesByChannelId(int channelId, CancellationToken token);
    }
}
