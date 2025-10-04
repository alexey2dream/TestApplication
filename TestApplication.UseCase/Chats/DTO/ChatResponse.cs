using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.UseCase.Chats.DTO
{
    public class ChatResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CreatorId { get; set; }
    }
}
