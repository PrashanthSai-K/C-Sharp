using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.Config.Migrations;

public class TaskItemConfig : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.HasData
        (
            new TaskItem
            {
                Id = 1,
                Name = "Sample task",
                Description = "to completed the sample task given",
                IsCompleted = false,
                DueDate = DateTime.Now,
                UserId = 1
            },
            new TaskItem
            {
                Id = 2,
                Name = "Demo task",
                Description = "to completed the demo task given",
                IsCompleted = false,
                DueDate = DateTime.Now,
                UserId = 1
            },
            new TaskItem
            {
                Id = 3,
                Name = "test task",
                Description = "to completed the test task given",
                IsCompleted = false,
                DueDate = DateTime.Now,
                UserId = 2
            },
            new TaskItem
            {
                Id = 4,
                Name = "example task",
                Description = "to completed the example task given",
                IsCompleted = false,
                DueDate = DateTime.Now,
                UserId = 2
            }
        );
    }
}
