using System;

namespace Company.CRM
{
    class Program
    {
        static void Main()
        {
            var runner = new Runner();
            
            runner.RunSimpleFailExample();
            runner.RunSimpleSuccessExample();
            runner.RunGetProductsExample();

            Console.ReadLine();
        }
    }
}
