using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Chats.Repositories;
using TestApplication.Infrastructure.Databases.Write;

namespace TestApplication.Infrastructure.Data.Write.Repositories
{
    public class ChatRep : IChatRep
    {
        private readonly AppDbContext context;
        public ChatRep(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(Chat chat, CancellationToken token = default)
        {
            if (chat == null)
                return false;
            await context.Chats.AddAsync(chat, token);
            return true;
        }

        public async Task<Chat> GetById(int id, CancellationToken token = default)
        {
            return await context.Chats
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync(token);
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
