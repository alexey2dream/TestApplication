using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Chats.Repositories
{
    public interface IChatRep
    {
        Task<Chat> GetById(int id, CancellationToken token = default);
        Task<bool> Add(Chat chat, CancellationToken token = default);
        int Save();

    }
}
