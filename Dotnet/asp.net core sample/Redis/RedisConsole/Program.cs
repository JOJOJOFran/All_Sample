using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace RedisConsole
{
    class Program
    {
        static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        static IDatabase db = redis.GetDatabase(0);
        static List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static void Main(string[] args)
        {
            //Redis 有多db
           
            var first=db.ListGetByIndex("order", 0);
            var firstOrder = db.ListLeftPop("order");




            for (int i = 0; i < 10; i++)
            {
                db.ListLeftPush("order", i);
            }

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"第{i}次，同步从Redis获取订单：{ db.ListLeftPop("order")}");
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine($"第{i}次，同步从内存列表获取订单：{list.FirstOrDefault()}");
            //    list.RemoveAt(0);
            //}

            for (int i = 0; i < 10000; i++)
            {
                db.ListLeftPush("order", i);
            }

            for (int i = 0; i < 10000; i++)
            {
                list.Add(i);
            }

            Thread.Sleep(5000);

            Parallel.For(0, 9999, i => GetOrder(i));
           
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread thread1 = new Thread(GetOrder);
            //    thread1.Start();
            //}



            //string value = "console";
            //db.StringSet("mykey", value);
            //db.StringSet("mykey1", "111");
            //db.StringSet("mykey2", "11221");



            //IDatabase db1 = redis.GetDatabase(1);
            //db1.HashSet("myhash", "key1", "value1");
            //Console.WriteLine (db.StringGet("mykey"));


            //ISubscriber sub = redis.GetSubscriber();
            //sub.Subscribe("message", (channel, message) => 
            //{
            //    Console.WriteLine(message);
            //});

            ////获取单个服务
            //IServer redisServer = redis.GetServer("localhost", 6379);

            //foreach (var key in redisServer.Keys(pattern: "*key*"))
            //{
            //    Console.WriteLine(key);
            //}
            System.Net.EndPoint[] endpoints = redis.GetEndPoints();
            Console.ReadKey();


        }

        static void GetOrder(int i)
        {
            var firstOrder = db.ListLeftPop("order");
            Console.WriteLine($"第{i}次，异步从Redis获取订单：{firstOrder}");
            
            //Console.WriteLine($"第{i}次，异步从内存列表获取订单：{list.FirstOrDefault()}");
            //list.RemoveAt(0);
        }
    }
}
