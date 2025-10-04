using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users.Models;
using TestApplication.Infrastructure.Databases.Read;
using TestApplication.UseCase.Chats.DTO;
using TestApplication.UseCase.Chats.QueryReps;
using TestApplication.UseCase.Users.DTO;

namespace TestApplication.Infrastructure.Data.Read.QueryRepositories
{
    public class ChatQueryRep : IChatQueryRep
    {
        private IDbConnectionFactory connectionFactory;
        public ChatQueryRep(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<List<ChatResponse>> GetAllByUserId(int userId, int pageNum, int pageSize, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Chats"
                where "CreatorId" = @CreatorId
                limit @Limit offset @Offset;
                """;
            var chats = await connection.QueryAsync<ChatResponse>(sql, new
            {
                Limit = pageSize * pageNum,
                Offset = (pageNum - 1) * pageSize,
                CreatorId = userId
            });
            return chats.ToList();
        }
        public async Task<ChatInfoResponse> GetByIdWithAllPages(int chatId, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Chats" c
                join "ChatsMessages" cM on cM."ChatId" = c."Id"
                where c."Id" = @ChatId;
                """;
            Dictionary<int, ChatInfoResponse> chatResponseDictionary = new Dictionary<int, ChatInfoResponse>();
            var chats = await connection.QueryAsync<ChatInfoResponse, ChatMessageResponse, ChatInfoResponse>(sql,
                (chat, message) =>
                {
                    if(chatResponseDictionary.TryGetValue(chatId, out var existingChat))
                        chat = existingChat;
                    else
                        chatResponseDictionary.Add(chat.Id, chat);
                    chat.Messages.Add(message);
                    return chat;
                },
                new{ChatId = chatId});
            return chatResponseDictionary.Count() != 0 ? chatResponseDictionary[chatId] : null;
        }
    }
}
