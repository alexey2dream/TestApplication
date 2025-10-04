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
