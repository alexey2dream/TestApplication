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
    public class ChannelRep : IChannelRep
    {
        private readonly AppDbContext context;
        public ChannelRep(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(Channel channel, CancellationToken token = default)
        {
            if (channel is null)
                return false;
            await context.Channels.AddAsync(channel, token);
            return true;
        }
        public int Save()
        {
            return context.SaveChanges();
        }
        public async Task<bool> IsTitleUnique(string title, CancellationToken token = default)
        {
            return !await context.Channels
                .Where(c => c.Title.ToLower() == title.ToLower())
                .AnyAsync(token);
        }
    }
}
