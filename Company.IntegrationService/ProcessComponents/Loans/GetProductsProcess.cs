using System;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.ProductManagement.Client;
using Company.IntegrationService.Contracts.DataContracts;
using System.Collections.Generic;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class GetProductsProcess : IProcessComponent<GetProductsRequest, GetProductsResponse>
    {
        private readonly IProductsClientProxy productsClient = null;
        
        public GetProductsProcess(IProductsClientProxy products)
        {
            if (products == null)
                throw new ArgumentNullException("products");

            this.productsClient = products;
        }

        public GetProductsResponse Process(GetProductsRequest request)
        {
            var response = new GetProductsResponse { ProductNames = new List<ProductName>() };
            
            if (request.Filter == ProductFilterClause.None)
                return response;

            var productList = productsClient.GetAllProducts();

            var productNames = new GetProductsOutputMapper().Map(productList);

            ApplyFilterClause(productNames, request.Filter);

            response.ProductNames = productNames;
            
            return response;
        }

        private static void ApplyFilterClause(IList<ProductName> productNames, ProductFilterClause clause)
        {
            if (clause == ProductFilterClause.Some)
            {
                // A silly business rule to highlight something we might do as a step...
                // randomly remove the first product name is ProductFilterClause.Some was selected
                productNames.Remove(productNames[0]);
            }
        }

    }
}
