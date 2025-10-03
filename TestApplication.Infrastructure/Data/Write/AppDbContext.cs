using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Domain.Channels.Models;
using TestApplication.Domain.Chats.Models;
using TestApplication.Domain.Users.Models;
using TestApplication.Infrastructure.Databases.Write.Configurations;

namespace TestApplication.Infrastructure.Databases.Write
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        private readonly ILoggerFactory loggerFactory;
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatsMessages { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelMessage> ChannelsMessages { get; set; }
        public AppDbContext(
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            this.configuration = configuration;
            this.loggerFactory = loggerFactory;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .UseNpgsql(configuration.GetConnectionString("Postgresql"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());
        }
    }
}
