using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.DTO
{
    public class ChatInfoResponse
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int CreatorId { get; private set; }
        public List<ChatMessageResponse> Messages { get; set; } = new List<ChatMessageResponse>();
    }
}
