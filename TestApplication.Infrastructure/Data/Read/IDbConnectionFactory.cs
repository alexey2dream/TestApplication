using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Infrastructure.Databases.Read
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnection(CancellationToken token = default);
    }
}
