using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        private Chat(string title, int creatorId)
        {
            Title = title;
            CreatorId = creatorId;
        }
        internal static Result<Chat> Create(string title, User creator)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result<Chat>.Failure("Title is null or empty!");
            if (creator is null)
                return Result<Chat>.Failure("Creator is null!");
            return Result<Chat>.Success(new Chat(title, creator.Id));
        }
        internal Result AddParticipant(User user)
        {
            if (user is null)
                return Result.Failure("User is null!");
            if(user.Id == CreatorId)
                return Result.Failure("Can't add creator as participant!");
            if(participants.Contains(user))
                return Result.Failure("User already added to chat!");
            participants.Add(user);
            return Result.Success();
        }
        internal Result ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                return Result.Failure("NewTitle is null or empty!");
            Title = newTitle;
            return Result.Success();
        }

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
