using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
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
        private ChatMessage(string text, DateTime sendingTime, int senderId, int chatId)
        {
            Text = text;
            SenderId = senderId;
            ChatId = chatId;
            SendingTime = sendingTime;
        }
        internal static Result<ChatMessage> Create(string text, User sender, Chat chat)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Result<ChatMessage>.Failure("Text is null or empty!");
            if (sender is null)
                return Result<ChatMessage>.Failure("Sender is null!");
            if (chat is null)
                return Result<ChatMessage>.Failure("Chat is null!");
            return Result<ChatMessage>.Success(new ChatMessage(text, DateTime.UtcNow, sender.Id, chat.Id));
        }

    }
}
