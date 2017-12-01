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

            services.AddMvc();
            services.AddDbContext<zModel>(options => options.UseSqlServer(ModuleHelper.ConnectionString));
            services.AddScoped(typeof(IRepositoryCollection), typeof(RepositoryCollection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ModuleHelper.ApplicationBuilder = app;
            ModuleHelper.HostingEnvironment = env;
            ModuleHelper.ServiceScope= app.ApplicationServices.CreateScope();

            GetPrimaryKey();

            app.UseStaticFiles();
            app.UseMvc();
        }

        void GetPrimaryKey()
        {
            zModel db = new zModel();
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
