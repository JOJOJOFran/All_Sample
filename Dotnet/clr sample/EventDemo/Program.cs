using System;

namespace EventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MailManage mm = new MailManage();
            Fax fax = new Fax(mm);
            mm.SimulateNewMail("sss", "sss", "sss");
            Console.ReadKey();
        }
    }
}
