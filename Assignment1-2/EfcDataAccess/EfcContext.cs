using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class EfcContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Post> posts { get; set; }
    public DbSet<Comment> comments{ get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<Post>().HasKey(post => post.Id);
         modelBuilder.Entity<User>().HasKey(user => user.UserName);
         modelBuilder.Entity<Comment>().HasKey(comment => comment.Id);
     }
  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/someReddit.db");
    }
    
        


}