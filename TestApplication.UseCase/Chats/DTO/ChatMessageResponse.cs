using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users.Models;

namespace TestApplication.UseCase.Chats.DTO
{
    public class ChatMessageResponse
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime SendingTime { get; private set; }
        public int SenderId { get; private set; }
    }
}
