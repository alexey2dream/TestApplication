using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Results;
using TestApplication.Domain.Users.Models;
using TestApplication.Domain.Users.Repositories;

namespace TestApplication.Domain.Users.Services
{
    public class UserService(IUserRep rep)
    {
        public async Task<Result<User>> Create(string username, CancellationToken token)
        {
            if (!await rep.IsUsernameUnique(username, token))
                return Result<User>.Failure("Username is taken!");
            var result = User.Create(username);
            if (!result.IsSuccess)
                return result;
            return result;
        }
        public async Task<Result> ChangeUsername(User user, string newUsername, CancellationToken token)
        {
            if(!await rep.IsUsernameUnique(newUsername, token))
                return Result<User>.Failure("Username is taken!");
            var result = user.ChangeUsername(newUsername);
            if (!result.IsSuccess)
                return result;
            return result;
        }
    }
}
