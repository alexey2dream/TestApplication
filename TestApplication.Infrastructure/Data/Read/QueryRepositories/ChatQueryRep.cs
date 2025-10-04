using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
