using System;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.ProductManagement.Client;
using Company.IntegrationService.Contracts.DataContracts;
using System.Collections.Generic;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    // 1 component per operation
    public class GetProductsProcess : IProcessComponent<GetProductsRequest, GetProductsResponse>
    {
        // Manages all of its own dependencies
        private readonly IProductsClientProxy productsClient = null;
        private readonly GetProductsProcessMappings mappings;

        public GetProductsProcess(IProductsClientProxy products)
        {
            if (products == null)
                throw new ArgumentNullException("products");

            this.productsClient = products;
            mappings = new GetProductsProcessMappings();
        }

        // The process operation runs all of the steps to process the request
        public GetProductsResponse Process(GetProductsRequest request)
        {
            var response = new GetProductsResponse { ProductNames = new List<ProductName>() };
            
            if (request.Filter == ProductFilterClause.None)
                return response;

            var productList = productsClient.GetAllProducts();

            var productNames = mappings.MapFromProductIdentifierListToProductNameList(productList);

            if (request.Filter == ProductFilterClause.Some)
            {
                productNames.Remove(productNames[0]); // randomly remove the first product name is ProductFilterClause.Some was selected
            }

            response.ProductNames = productNames;
            
            return response;
        }

    }
}
