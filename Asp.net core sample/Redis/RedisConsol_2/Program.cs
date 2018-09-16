using System;
using StackExchange.Redis;

namespace RedisConsol_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis =  ConnectionMultiplexer.Connect("localhost");
            ISubscriber sub = redis.GetSubscriber();
            sub.Publish("message", "Hello guys");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
