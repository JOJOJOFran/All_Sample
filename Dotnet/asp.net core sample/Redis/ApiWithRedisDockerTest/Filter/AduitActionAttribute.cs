using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithRedisDockerTest.Filter
{
    public class AduitAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 执行任务开始前
        /// </summary>
        /// <param name="context"></param>
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{

        //    Console.WriteLine($"执行{context.Controller.ToString()}开始");

        //    base.OnActionExecuting(context);
        //}

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"执行OnActionExecutionAsync中");
            return base.OnActionExecutionAsync(context, next);
        }

        //public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        //{
        //    Console.WriteLine($"执行结束{context.Result.ToString()}");
        //    return base.OnResultExecutionAsync(context, next);
        //}

        /// <summary>
        /// 执行任务完成时记录
        /// </summary>
        /// <param name="context"></param>
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    Console.WriteLine($"执行{context.Controller.ToString()}结束");
        //    base.OnActionExecuted(context);
        //}
    }
}
