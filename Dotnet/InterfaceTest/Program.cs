using System;

namespace InterfaceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext dbContext = null;
            dbContext = (DbContext)GetDbContext();
            dbContext.SaveChangs();
            Console.ReadKey();
        }

        public static IDbContext GetDbContext()
        {
            return new DbContextTest();
        }
    }
}
