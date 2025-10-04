using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Channels.Services
{
    public class ChannelService
    {
        private readonly IChannelRep rep;
        public ChannelService(IChannelRep rep)
        {
            this.rep = rep;
        }
        public async Task<Result<Channel>> Create(string title, User creator, CancellationToken token)
        {
            if (!await rep.IsTitleUnique(title))
                return Result<Channel>.Failure("Title is taken!");
            var result = Channel.Create(title, creator);
            if (!result.IsSuccess)
                return result;
            return result;
        }
    }
}
