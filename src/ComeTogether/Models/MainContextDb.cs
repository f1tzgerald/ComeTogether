using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Models;

namespace ComeTogether.Models
{
    public class MainContextDb : DbContext
    {
        public MainContextDb()
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Startup.Configuration["DataConnection:ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder); 
        }
    }
}
