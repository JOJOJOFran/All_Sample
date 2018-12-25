using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithRedisDockerTest.Filter
{
    public class DisAduitAttribute:AduitAttribute
    {
        public new Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return null;
        }

        public new Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return null; 
        }
    }
}
