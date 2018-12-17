using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseFactoryActivatedMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FactoryActivatedMiddleWare>();
        }

        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
