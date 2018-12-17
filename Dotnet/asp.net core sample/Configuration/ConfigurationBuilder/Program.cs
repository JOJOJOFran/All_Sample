using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConfigurationBuilder_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("json1.json");

            var config = builder.Build();
            var list= config.GetSection("report") .GetSection("user").GetChildren();

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
