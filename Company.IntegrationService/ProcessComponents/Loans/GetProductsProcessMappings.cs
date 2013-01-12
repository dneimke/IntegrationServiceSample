using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.Contracts.MessageContracts;
using DC = Company.IntegrationService.Contracts.DataContracts;
using Company.LOB.ProductManagement.Entities;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class GetProductsProcessMappings
    {
        /// <summary>
        /// Maps from an <see cref="IEnumerable<ProductIdentifier>"/> to a <see cref="GetProductsResponse"/>
        /// </summary>
        /// <param name="productList">The list of Product Management Products to map from</param>
        /// <returns>A GetProductsResponse message contract</returns>
        public IList<DC.ProductName> MapFromProductIdentifierListToProductNameList(IEnumerable<ProductIdentifier> productList)
        {
            var response = new GetProductsResponse();
            var productNames = from p in productList select new DC.ProductName(p.Id, p.Name);
            return productNames.ToList();
        }
    }
}
