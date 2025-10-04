using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Users.Models;
using TestApplication.Domain.Users.Repositories;
using TestApplication.Infrastructure.Databases.Write;

namespace TestApplication.Infrastructure.Data.Write.Repositories
{
    public class UserRep : IUserRep
    {
        private readonly AppDbContext context;
        public UserRep(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<User> GetById(int id, CancellationToken token = default)
        {
            return await context.Users
                .Include(u => u.Channel)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync(token);
        }
        public async Task<bool> Add(User user, CancellationToken token = default)
        {
            if (user == null)
                return false;
            await context.Users.AddAsync(user);
            return true;
        }
        public async Task<bool> Delete(User user, CancellationToken token = default)
        {
            if (user == null)
                return false;
            context.Users.Remove(user);
            return true;
        }
        public int Save()
        {
            return context.SaveChanges();
        }
        public async Task<bool> IsUsernameUnique(string username, CancellationToken token = default)
        {
            return !await context.Users
                .Where(u => u.Username.ToLower() == username.ToLower())
                .AnyAsync(token);
        }

    }
}
