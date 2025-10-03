using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats;
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
        //private Channel()
        //{
            
        //}

    }
}
