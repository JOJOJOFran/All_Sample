using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonApi.Repositories
{
    public class UserRepositories:ISqlServerRepositories,IMySqlRepositories
    {
        public void Add()
        {
            Console.WriteLine("用户执行Add方法");
        }

        public void Delete()
        {
            Console.WriteLine("用户执行Delete方法");
        }

        public void Update()
        {
            Console.WriteLine("用户执行Update方法");
        }

        public void Select()
        {
            Console.WriteLine("用户执行Select方法");
        }

    }
}
