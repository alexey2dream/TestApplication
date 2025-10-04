using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;

namespace TestApplication.Domain.Channels.Repositories
{
    public interface IChannelRep
    {
        Task<bool> Add(Channel channel, CancellationToken token = default);
        int Save();
        Task<bool> IsTitleUnique(string title, CancellationToken token = default);

    }
}
