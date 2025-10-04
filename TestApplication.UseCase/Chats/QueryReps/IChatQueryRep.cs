using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.UseCase.Chats.DTO;

namespace TestApplication.UseCase.Chats.QueryReps
{
    public interface IChatQueryRep
    {
        Task<List<ChatResponse>> GetAllByUserId(int userId, int pageNum, int pageSize, CancellationToken token);
    }
}
