using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Middleware.Data;
using Middleware.Middleware;

namespace Middleware
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemotyDb");
            });

            //这里必须注入
            services.AddScoped<FactoryActivatedMiddleWare>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // RequestDelegate  一个HttpContext作为参数，Task作为返回值的委托

            //Parameter: Func<RequestDelegate,RequestDelegate> 
            //Reload: Func<RequestDelegate,Func<Task>,Task>

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Let's begining learning Middleware! \n");
            //    await next.Invoke();
            //});


            ////Parameter:PathString,Action<IApplicationBuilder>
            //app.Map("/map1", HandleMapTest1);
            //app.Map("/map2", HandleMapTest2);
            ////Parameter: Func<HttpContext,bool> Action<IApplicationBuilder>
            ////http://localhost:3237/?branch=master
            //app.MapWhen(context => 
            //context.Request.Query.ContainsKey("branch"), HandlerBranch);

            //app.Use((context, next) =>
            //{
            //    var cultureQuery = context.Request.Query["culture"];
            //    if (!String.IsNullOrEmpty(cultureQuery))
            //    {
            //        var culture = new CultureInfo(cultureQuery);

            //        CultureInfo.CurrentCulture = culture;
            //        CultureInfo.CurrentUICulture = culture;
            //    }

            //    return next();

            //});

            //app.Use(async (context,next) =>
            //{
            //    await context.Response.WriteAsync(
            //        $"Hello {CultureInfo.CurrentCulture.DisplayName}");
            //    await next.Invoke();
            //});


            //app.UseRequestCulture();

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync(
            //        $"Hello {CultureInfo.CurrentCulture.DisplayName}");
            //    await next.Invoke();
            //});



            //app.Use(async (context ,next)=>
            //{
            //    await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
            //    await next.Invoke();
            //});

            //Parameter: RequestDelegate  
            //HttpContext=》using Microsoft.AspNetCore.Http
            //第一个 <xref:Microsoft.AspNetCore.Builder.RunExtensions.Run*> 委托终止了管道。
            //这里的app.Run和app.UseMvc都会终止管道，所以只会按顺序执行前面的一个

            //如果在UseMvc()之前写入context.Response.WriteAsync，会对response头重复设置 而它是只读的  会报错
            app.UseFactoryActivatedMiddleWare();
            app.UseMvc();




        }

        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }

        private static void HandlerBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }
    }
}
