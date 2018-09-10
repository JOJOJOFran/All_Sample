using System;
using StackExchange.Redis;

namespace RedisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            string value = "console";
            db.StringSet("mykey", value);
            Console.WriteLine (db.StringGet("mykey"));
            Console.ReadKey();
        }
    }
}
