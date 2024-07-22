using Microsoft.EntityFrameworkCore;
using TwUsers.Models;

namespace TwUsers.Context;

public class TwUsersContext : DbContext{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=blazor_db;Uid=root;Pwd=123456;");
    }
}