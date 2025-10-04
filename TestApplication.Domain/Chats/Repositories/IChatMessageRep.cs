using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Models;

namespace TestApplication.Domain.Chats.Repositories
{
    public interface IChatMessageRep
    {
        Task<ChatMessage> GetById(int id, CancellationToken token = default);
    }
}
