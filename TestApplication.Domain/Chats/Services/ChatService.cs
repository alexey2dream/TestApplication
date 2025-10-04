using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Chats.Services
{
    public class ChatService
    {
        public Result<Chat> Create(string title, User creator, List<User> participants)
        {
            var result = Chat.Create(title, creator);
            if (!result.IsSuccess)
                return result;
            foreach(var u in participants)
            {
                var addChatParticipantResult = result.Value.AddParticipant(u);
                if (!addChatParticipantResult.IsSuccess)
                    return Result<Chat>.Failure(addChatParticipantResult.Error);
            }
            return result;
        }
        public Result Delete(User creator, Chat chat)
        {
            var deleteResult = creator.DeleteChat(chat);
            if (!deleteResult.IsSuccess)
                return deleteResult;
            return deleteResult;
        } 
        public Result DeleteMessage(Chat chat, ChatMessage message)
        {
            var deleteResult = chat.DeleteMessage(message);
            if (!deleteResult.IsSuccess)
                return deleteResult;
            return deleteResult;
        }
    }
}
