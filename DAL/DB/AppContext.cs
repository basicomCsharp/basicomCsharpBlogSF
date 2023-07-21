using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogSF
{
    public class AppContext: DbContext
    {
        // Объекты таблиц
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppContext()
        {
           // Database.EnsureDeleted();
           // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<Comment>().ToTable("Comments");
            builder.Entity<Role>().ToTable("Roles");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\SF\Mvc\blog.db");            
        }
    }
}
