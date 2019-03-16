using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConfigurationBuilder_Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            var index = Array.IndexOf(args, "/env");
            var environment = index > -1 ? args[index + 1] : "staging";

            //构建IConfigurationBuilder对象，并添加内存配置源
            var builder = new ConfigurationBuilder().Add(new MemoryConfigurationSource { });
            //添加jsonFile作为配置源
            //builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"json1.{environment}.json");
            //通过option配置热更新
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: $"json1.staging.json",
                                                                                optional: true,
                                                                                reloadOnChange: true).Build();

            //构建IConfiguration对象
            var config = builder.Build();

            ChangeToken.OnChange(() => config.GetReloadToken(), () => {
                Console.WriteLine("onChange");
                string myDb1 = config["connectstring:sqlserver"];
                Console.WriteLine(myDb1);

            });

            
            //通过GetSection读取
            var list= config.GetSection("report") .GetSection("user").GetChildren();
            //通过":"读取
            var myDb = config["connectstring:sqlserver"];
            Console.WriteLine(myDb);
            List<HeaderTitle> titles = new List<HeaderTitle>();
            foreach (var item in list)
            {
                titles.Add(new HeaderTitle
                {
                    Title = item["title"],
                    Width = Convert.ToInt32(item["width"])
                     
                });
                Console.WriteLine(item["title"]);
                Console.WriteLine(item["width"]);
            }


           
            Console.ReadKey();
        }
    }
}
