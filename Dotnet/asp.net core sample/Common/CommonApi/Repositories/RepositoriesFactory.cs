using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonApi.Repositories
{
    public static class RepositoriesFactory
    {
        public static IRepositories GetRepositories(this IRepositories repositories, string type)
        {
            switch (type)
            {
                case "User":
                    return new UserRepositories();
                case "Order":
                    return new OrderRepositories();
            }
            return null;
        }
    }
}
