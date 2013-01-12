using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.FrontEndSystems.LoanApplicationSystem.Examples;

namespace Company.FrontEndSystems.LoanApplicationSystem
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
