using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;

namespace TestApplication.Domain.Channels.Repositories
{
    public interface IChannelMessageRep
    {
        Task<ChannelMessage> GetById(int id, CancellationToken token = default);
    }
}
