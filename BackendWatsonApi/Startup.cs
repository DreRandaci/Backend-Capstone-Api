using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendWatsonApi.Models;
using BackendWatsonApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BackendWatsonApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /**
                Create a service for DI that will return the ApplicationConfiguration
                section of appsettings.
             */
            services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>(
                e => Configuration.GetSection("ApplicationConfiguration")
                        .Get<ApplicationConfiguration>());

            services.AddMvc()

            // Add reference loop ignore for GET requests
               .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // define path to database           
            var connection = "Data Source=C:/Users/drera_000/Documents/Workspace/BackendWatsonApi/BackendWatsonApi/api.db";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
