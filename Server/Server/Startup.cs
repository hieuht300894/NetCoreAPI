using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server.Service;
using Server.Model;
using Microsoft.EntityFrameworkCore;
using Server.Utils;
using Microsoft.EntityFrameworkCore.Metadata;
using Server.Middleware;
using Microsoft.AspNetCore.Http;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ModuleHelper.Configuration = Configuration;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ModuleHelper.ServiceCollection = services;
            ModuleHelper.ConnectionString = Configuration.GetConnectionString("QuanLyBanHangModel");

            services.AddDbContext<aModel>(options => options.UseSqlServer(ModuleHelper.ConnectionString));
            services.AddScoped(typeof(IRepositoryCollection), typeof(RepositoryCollection));
            services.AddScoped<Filter>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ModuleHelper.ApplicationBuilder = app;
            ModuleHelper.HostingEnvironment = env;
            ModuleHelper.ServiceScope = app.ApplicationServices.CreateScope();

            GetPrimaryKey();

            app.UseStaticFiles();
            app.UseMvc();
        }

        void GetPrimaryKey()
        {
            aModel db = new aModel();
            ModuleHelper.ListKeys = new List<IKey>();
            var eTypes = db.Model.GetEntityTypes();
            foreach (var eType in eTypes)
            {
                var keys = eType.GetKeys();
                foreach (var key in keys)
                {
                    ModuleHelper.ListKeys.Add(key);
                }
            }
        }
    }
}
