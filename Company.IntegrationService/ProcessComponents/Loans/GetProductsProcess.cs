using System;
using System.Collections.Generic;
using System.Linq;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.ProductManagement.Client;
using Company.LOB.ProductManagement.Entities;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    // 1 component per operation
    public class GetProductsProcess : IProcessComponent<GetProductsRequest, GetProductsResponse>
    {
        // Manages all of its own dependencies
        private readonly ProductsClientProxy products = null;

        public GetProductsProcess(ProductsClientProxy products)
        {
            if (products == null)
                throw new ArgumentNullException("products");

            this.products = products;
        }

        // The process operation runs all of the steps to process the request
        public GetProductsResponse Process(GetProductsRequest request)
        {
            var availableProducts = GetAvailableProductsCallStep(request);
            
            decimal madeUpDecimal;
            char madeUpChar;

            PrepareInputsForGetProductsCallStep(availableProducts, out madeUpDecimal, out madeUpChar);
            
            var productList = GetAllProductsCallStep(availableProducts, madeUpDecimal, madeUpChar);
            
            var response = PrepareResponseStep(productList);
            
            return response;
        }

        #region Everything here is the logic for the steps that are carried out by this process component
        

        /// <summary>
        /// Describe the step and why it exists, what it does, etc
        /// </summary>
        private static GetProductsResponse PrepareResponseStep(IEnumerable<Product> productList)
        {
            var response = new GetProductsResponse();
            var productNames = from p in productList select p.Name.Name;
            response.ProductNames = productNames.ToList();
            return response;
        }

        /// <summary>
        /// Describe the step and why it exists, what it does, etc
        /// </summary>
        private IEnumerable<Product> GetAllProductsCallStep(int availableProducts, decimal madeUpDecimal, char madeUpChar)
        {
            var productList = products.GetAllProducts(availableProducts, madeUpDecimal, madeUpChar);
            return productList;
        }

        /// <summary>
        /// Describe the step and why it exists, what it does, etc
        /// </summary>
        private static void PrepareInputsForGetProductsCallStep(int availableProducts, out decimal madeUpDecimal, out char madeUpChar)
        {
            madeUpDecimal = (decimal)availableProducts;
            madeUpChar = (char)availableProducts;

            if (availableProducts > 5)
            {
                madeUpDecimal = 1;
                madeUpChar = 'a';
            }
        }

        /// <summary>
        /// Describe the step and why it exists, what it does, etc
        /// </summary>
        private int GetAvailableProductsCallStep(GetProductsRequest request)
        {
            var query = request.RequestedProducts.ToString();
            var availableProducts = products.ProductAvailability(query);
            return availableProducts;
        }

        #endregion

    }
}
