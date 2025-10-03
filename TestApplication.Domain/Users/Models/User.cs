using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;
using TestApplication.Domain.Chats.Models;

namespace TestApplication.Domain.Users.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        private readonly List<Chat> createdChats = new List<Chat>();
        public IReadOnlyCollection<Chat> CreatedChats => createdChats.AsReadOnly();
        private readonly List<Chat> joinedChats = new List<Chat>();
        public IReadOnlyCollection<Chat> JoinedChats => joinedChats.AsReadOnly();
        public Channel? Channel { get; private set; }
        private User() { }
        //private User()
        //{
        //}

    }
}
