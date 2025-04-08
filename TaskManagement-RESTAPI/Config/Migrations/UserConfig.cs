using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.Config.Migrations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.HasData
        (
            new User
            {
                Id = 1,
                Username = "test1",
                Email = "test1@gmail.com",
                Mobile = "0987654321",
                Password = "password",
                Role = "User"
            },
            new User
            {
                Id = 2,
                Username = "test2",
                Email = "test2@gmail.com",
                Mobile = "0987654321",
                Password = "password",
                Role = "User"
            },
            new User
            {
                Id = 3,
                Username = "admin",
                Email = "admin@gmail.com",
                Mobile = "0987654321",
                Password = "password",
                Role = "Admin"
            }
        );
    }
}
