using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using ComeTogether.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using ComeTogether.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using ComeTogether.ViewModels;

namespace ComeTogether
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("configuration.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
                );

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MainContextDb>();

            services.AddScoped<IMailService, DebugMailService>();
            services.AddScoped<ITasksRepository, TasksRepository>();

            services.AddTransient<SeedData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, SeedData dataToAdd)
        {
            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Main", action = "Index" });
            });

            // Add data to database if not exists
            dataToAdd.AddData();

            Mapper.Initialize(config =>
            {
                config.CreateMap<CategoryViewModel, Category>().ReverseMap();
                config.CreateMap<ToDoItemViewModel, TodoItem>().ReverseMap();
                config.CreateMap<CommentViewModel, Comment>().ReverseMap();
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
