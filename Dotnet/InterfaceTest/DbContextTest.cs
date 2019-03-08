using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceTest
{
    public class DbContextTest : DbContext, IDbContext
    {
        public void Rollback()
        {
            Console.WriteLine("IDbContext=>Rollback");
        }
    }
}
