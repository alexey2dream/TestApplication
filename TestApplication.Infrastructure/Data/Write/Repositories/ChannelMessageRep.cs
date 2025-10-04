using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;
using TestApplication.Domain.Channels.Repositories;
using TestApplication.Infrastructure.Databases.Write;

namespace TestApplication.Infrastructure.Data.Write.Repositories
{
    public class ChannelMessageRep : IChannelMessageRep
    {
        private readonly AppDbContext context;
        public ChannelMessageRep(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ChannelMessage> GetById(int id, CancellationToken token = default)
        {
            return await context.ChannelsMessages
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync(token);
        }
    }
}
