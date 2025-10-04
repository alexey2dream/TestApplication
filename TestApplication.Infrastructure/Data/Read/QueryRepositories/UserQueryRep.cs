using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Infrastructure.Databases.Read;
using TestApplication.UseCase.Users.DTO;
using TestApplication.UseCase.Users.QueryReps;

namespace TestApplication.Infrastructure.Data.Read.QueryRepositories
{
    public class UserQueryRep : IUserQueryRep
    {
        private IDbConnectionFactory connectionFactory;
        public UserQueryRep(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<List<UserResponse>> GetAll(int pageNum, int pageSize, CancellationToken token = default)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Users"
                limit @Limit offset @Offset;
                """;
            var users = await connection.QueryAsync<UserResponse>(sql, new {
                Limit = pageSize * pageNum,
                Offset = (pageNum - 1) * pageSize});
            return users.ToList();
        }
        public async Task<UserResponse> GetById(int id, CancellationToken token)
        {
            using var connection = await connectionFactory.CreateConnection(token);
            string sql = """
                select * from "Users"
                where "Id" = @Id;
                """;
            var user = await connection.QueryAsync<UserResponse>(sql, new
            {
                Id = id
            });
            return user.FirstOrDefault();
        }
    }
}
