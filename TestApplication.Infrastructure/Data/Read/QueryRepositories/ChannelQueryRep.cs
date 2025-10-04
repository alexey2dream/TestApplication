using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Infrastructure.Databases.Read;
using TestApplication.UseCase.Channels.DTO;
using TestApplication.UseCase.Channels.QueryReps;
using TestApplication.UseCase.Users.DTO;

namespace TestApplication.Infrastructure.Data.Read.QueryRepositories
{
    public class ChannelQueryRep : IChannelQueryRep
    {
        private IDbConnectionFactory connectionFactory;
        public ChannelQueryRep(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<List<ChannelResponse>> GetAllByUserId(int pageNum, int pageSize, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Channels"
                limit @Limit offset @Offset;
                """;
            var channels = await connection.QueryAsync<ChannelResponse>(sql, new
            {
                Limit = pageSize * pageNum,
                Offset = (pageNum - 1) * pageSize
            });
            return channels.ToList();
        }
    }
}
