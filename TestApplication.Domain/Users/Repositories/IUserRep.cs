using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Domain.Users.Repositories
{
    public interface IUserRep
    {
        Task<User> GetById(int id, CancellationToken token = default);
        Task<bool> Add(User user, CancellationToken token = default);
        //Task<bool> Add(User user, CancellationToken token = default);
        int Save();
        Task<bool> IsUsernameUnique(string username, CancellationToken token = default);

    }
}
