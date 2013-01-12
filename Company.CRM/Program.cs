using System;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.CRM
{
    class Program
    {
        static void Main()
        {
            var runner = new Runner();

            runner.RunGetProductsRequest(ProductFilterClause.All);
            runner.RunGetProductsRequest(ProductFilterClause.Some);
            runner.RunGetProductsRequest(ProductFilterClause.None);

            Console.ReadLine();
        }
    }
}
