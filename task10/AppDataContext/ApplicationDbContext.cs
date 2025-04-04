using System;
using Microsoft.EntityFrameworkCore;
using task10.Models;

namespace task10.AppDataContext;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;database=sqltasks;user=root;password=;",
            new MySqlServerVersion(new Version(8, 0, 34)));
        }
    }
}
