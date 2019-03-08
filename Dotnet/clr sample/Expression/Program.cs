using System;
using System.Linq.Expressions;

namespace Expression
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Expression<Action<string>> lambda = v => Console.WriteLine(v);
            Console.WriteLine(lambda.ToString()); //输出 v => Console.WriteLine(v)
         
            var lambdaExpression = lambda as LambdaExpression;
         
            Console.WriteLine(Convert.ToString(lambdaExpression.Name));
            Console.WriteLine(Convert.ToString(lambdaExpression.NodeType));
            foreach (var parameter in lambdaExpression.Parameters)
            {
                Console.WriteLine("Name:{0}, Type:{1}, ", parameter.Name, parameter.Type.ToString());
            }
            Console.WriteLine(lambdaExpression.Body.ToString());

            Console.ReadKey();
        }
    }
}
