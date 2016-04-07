using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ComeTogether.DAL.Entities;
using Microsoft.Data.Entity.Metadata;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ComeTogether.DAL.EntityFramework
{
    public class ComeTogetherContext : IdentityDbContext<Person>
    {

        public ComeTogetherContext() : base ()
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<TodoItem> ToDoItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<CategoryPeople> CategoryPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cascade delete todoitems
            builder.Entity<TodoItem>().HasOne(c => c.Category)
                                      .WithMany(c => c.ToDoItems)
                                      .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete comments
            builder.Entity<Comment>().HasOne(c => c.ToDoItem)
                                     .WithMany(c => c.Comments)
                                     .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many config
            builder.Entity<CategoryPeople>().HasKey(t => new { t.CategoryId, t.UserId });

            builder.Entity<CategoryPeople>().HasOne(c => c.Category)
                                            .WithMany(c => c.CategoryPeople)
                                            .HasForeignKey(c => c.CategoryId);

            builder.Entity<CategoryPeople>().HasOne(c => c.Person)
                                            .WithMany(c => c.CategoryPeople)
                                            .HasForeignKey(c => c.UserId);
        }
    }
}
