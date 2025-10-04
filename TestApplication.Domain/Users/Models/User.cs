using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Results;

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
        private User(string username)
        {
            Username = username;
        }
        internal static Result<User> Create(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result<User>.Failure("Username is null or empty!");
            return Result<User>.Success(new User(username));
        }
        internal Result Delete(Chat chat)
        {
            if (!createdChats.Contains(chat))
                return Result.Failure("User not created that chat!");
            createdChats.Remove(chat);
            return Result.Success();
        } 
    }
}
