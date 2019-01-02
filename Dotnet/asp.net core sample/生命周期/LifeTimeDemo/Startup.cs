using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LifeTimeDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            CommonFuncs.OutPut("Call Startup");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CommonFuncs.OutPut("Configure-Services");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(() => CommonFuncs.OutPut("IApplicationLifetime.ApplicationStarted"));
            lifetime.ApplicationStopping.Register(() =>
            {
                CommonFuncs.OutPut("ApplicationLifetime - Stopping");
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                Thread.Sleep(5 * 1000);

                CommonFuncs.OutPut("ApplicationLifetime - Stopped");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            // For trigger stop WebHost
            var thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(10* 1000);
                CommonFuncs.OutPut("Trigger stop WebHost");
                lifetime.StopApplication();
            }));
            thread.Start();

            CommonFuncs.OutPut("Startup.Configure - Called");
        }
    }
}
