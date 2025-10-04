using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Infrastructure.Databases.Read;

namespace TestApplication.Infrastructure.Data.Read
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration configuration;
        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<IDbConnection> CreateConnection(CancellationToken token = default)
        {
            var connection = new NpgsqlConnection(configuration.GetConnectionString("Postgresql"));
            await connection.OpenAsync();
            return connection;
        }
    }
}
