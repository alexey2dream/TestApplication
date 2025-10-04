using System;
using System.Collections.Generic;
using System.Formats.Tar;
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
        public Result UpdateTitle(Chat chat, string newTitle)
        {
            var result = chat.ChangeTitle(newTitle);
            if (!result.IsSuccess) 
                return result;
            return result;
        }
        public Result Delete(User creator, Chat chat)
        {
            var deleteResult = creator.DeleteChat(chat);
            if (!deleteResult.IsSuccess)
                return deleteResult;
            return deleteResult;
        } 
        public Result CreateMessage(string text, User sender, Chat chat)
        {
            if (!chat.IsUserExists(sender))
                return Result.Failure("User not in chat!");
            var result = ChatMessage.Create(text, sender, chat);
            if (!result.IsSuccess)
                return result;
            var addChatMessageResult = chat.AddMessage(result.Value);
            if (!addChatMessageResult.IsSuccess)
                return addChatMessageResult;
            return result;
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
