using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ComeTogether.Models;

namespace ComeTogether.Migrations
{
    [DbContext(typeof(MainContextDb))]
    [Migration("20160321165554_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComeTogether.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ComeTogether.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Creator");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Text");

                    b.Property<int?>("TodoItemId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ComeTogether.Models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Creator");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("DateFinish");

                    b.Property<bool>("Done");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("WhoDoIt");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ComeTogether.Models.Comment", b =>
                {
                    b.HasOne("ComeTogether.Models.TodoItem")
                        .WithMany()
                        .HasForeignKey("TodoItemId");
                });

            modelBuilder.Entity("ComeTogether.Models.TodoItem", b =>
                {
                    b.HasOne("ComeTogether.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });
        }
    }
}
