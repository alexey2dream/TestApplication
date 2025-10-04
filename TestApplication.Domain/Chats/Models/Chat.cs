using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
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

        //internal Result AddMessage(ChatMessage message)
        //{
        //    if (message is null)
        //        return Result.Failure("Message is null!");
        //    messages.Add(message);
        //    return Result.Success();
        //}

        internal Result DeleteMessage(ChatMessage message)
        {
            if (!messages.Contains(message))
                return Result.Failure("Not that's chat message!");
            if(message.SendingTime.AddMinutes(1) < DateTime.UtcNow)
                return Result.Failure("Too late for message deleting!");
            messages.Remove(message);
            return Result.Success();
        }
    }
}
