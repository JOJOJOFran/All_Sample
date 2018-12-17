using Microsoft.AspNetCore.Http;
using Middleware.Data;
using Middleware.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class FactoryActivatedMiddleWare : IMiddleware
    {
        private readonly AppDbContext _db;

        public FactoryActivatedMiddleWare(AppDbContext db)
        {
            _db = db;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           context.Response.OnStarting((state) => { context.Response.ContentType = "application/json"; return Task.FromResult(0); },null);
           var keyValue = context.Request.Query["key"];
            if (!String.IsNullOrEmpty(keyValue))
            {
                _db.Add(new Request()
                {
                    DT = DateTime.UtcNow,
                    MiddlewareActivation = "FactoryActivatedMiddleware",
                    Value = keyValue
                });

                await _db.SaveChangesAsync();
            }
            await next(context);
        }
    }
}
