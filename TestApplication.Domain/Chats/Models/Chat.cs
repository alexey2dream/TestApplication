using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Chats.Models
{
    public class Chat
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int CreatorId { get; private set; }
        public User Creator { get; private set; }
        private readonly List<User> participants = new List<User>();
        public IReadOnlyCollection<User> Participants => participants.AsReadOnly();
        private readonly List<ChatMessage> messages = new List<ChatMessage>();
        public IReadOnlyCollection<ChatMessage> Messages => messages.AsReadOnly();
        private Chat() { }
        //private Chat()
        //{
            
        //}


    }
}
