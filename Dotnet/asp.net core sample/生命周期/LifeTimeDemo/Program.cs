using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LifeTimeDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CommonFuncs.OutPut("开始进入Main入口");
            var builder = CreateWebHostBuilder(args).Build();
            CommonFuncs.OutPut("构建WebHost");
            CommonFuncs.OutPut("准备运行WebHost");
            builder.Run();
            CommonFuncs.OutPut("运行WebHost，你猜会不会执行？");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            CommonFuncs.OutPut("准备构建WebHost");
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}
