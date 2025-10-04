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
    public class ChatMessageRep : IChatMessageRep
    {
        private readonly AppDbContext context;
        public ChatMessageRep(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ChatMessage> GetById(int id, CancellationToken token = default)
        {
            return await context.ChatsMessages
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync(token);
        }
    }
}
