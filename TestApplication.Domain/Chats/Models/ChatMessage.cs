using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Chats.Models
{
    public class ChatMessage
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime SendingTime { get; private set; }
        public int SenderId { get; private set; }
        public User Sender { get; private set; }
        public int ChatId { get; private set; }
        public Chat Chat { get; private set; }
        private ChatMessage() { }
        //private ChatMessage()
        //{
            
        //}

    }
}
