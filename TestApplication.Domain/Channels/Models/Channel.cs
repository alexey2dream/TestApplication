using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Channels.Models
{
    public class Channel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int CreatorId { get; private set; }
        public User Creator { get; private set; }
        private readonly List<ChannelMessage> messages = new List<ChannelMessage>();
        public IReadOnlyCollection<ChannelMessage> Messages => messages.AsReadOnly();
        private Channel() { }
        private Channel(string title, int creatorId)
        {
            Title = title;
            CreatorId = creatorId;
        }
        internal static Result<Channel> Create(string title, User creator)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result<Channel>.Failure("Title is null or empty!");
            if (creator is null)
                return Result<Channel>.Failure("Creator is null!");
            return Result<Channel>.Success(new Channel(title, creator.Id));
        }
        internal Result ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                return Result.Failure("NewTitle is null or empty!");
            Title = newTitle;
            return Result.Success();
        }
    }
}
