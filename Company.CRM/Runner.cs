using System;
using Company.IntegrationService.Contracts;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.WebService;

namespace Company.CRM
{
    class Runner
    {
        ILoansIntegrationService service;

        public Runner()
        {
            this.service = new LoansIntegrationServiceWithComponents();
        }


        internal void RunGetProductsRequest(ProductFilterClause filter)
        {
            Console.WriteLine("----------- Calling GetProducts with a filter of {0}. ------------", filter.ToString());

            var request = new GetProductsRequest
            {
                Filter = filter
            };

            var response = service.GetProducts(request);

            foreach (var product in response.ProductNames)
            {
                Console.WriteLine("Retrieved {1} which has an Id of {0}", product.Id, product.Name);
            }

            Console.WriteLine();
        }

    }
}
