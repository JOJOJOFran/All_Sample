using System;
using StackExchange.Redis;

namespace RedisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Redis 有多db
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            string value = "console";
            db.StringSet("mykey", value);
            db.StringSet("mykey1", "111");
            db.StringSet("mykey2", "11221");

            

            IDatabase db1 = redis.GetDatabase(1);
            db1.HashSet("myhash", "key1", "value1");
            Console.WriteLine (db.StringGet("mykey"));
            

            ISubscriber sub = redis.GetSubscriber();
            sub.Subscribe("message", (channel, message) => 
            {
                Console.WriteLine(message);
            });

            //获取单个服务
            IServer redisServer = redis.GetServer("localhost", 6379);

            foreach (var key in redisServer.Keys(pattern: "*key*"))
            {
                Console.WriteLine(key);
            }
            System.Net.EndPoint[] endpoints = redis.GetEndPoints();
            Console.ReadKey();


        }
    }
}
