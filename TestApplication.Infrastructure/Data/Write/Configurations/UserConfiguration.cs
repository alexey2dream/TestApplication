using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApplication.Domain.Channels.Models;
using TestApplication.Domain.Users.Models;

namespace TestApplication.Infrastructure.Databases.Write.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.CreatedChats)
                .WithOne(c => c.Creator)
                .HasForeignKey(c => c.CreatorId)
                .IsRequired();
            builder
                .HasMany(u => u.JoinedChats)
                .WithMany(c => c.Participants);
            builder
                .HasOne(u => u.Channel)
                .WithOne(c => c.Creator)
                .HasForeignKey<Channel>(c => c.CreatorId)
                .IsRequired();

        }
    }
}
