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
        /// Maps from an <see cref="IEnumerable"/> of Product Management <see cref="ProductIdentifier"/> 
        /// to an <see cref="IList"/> of <see cref="ProductName"/> items.
        /// </summary>
        /// <param name="productList">An <see cref="IEnumerable"/> of Product Management 
        /// <see cref="ProductIdentifier"/> items to map from
        /// </param>
        /// <returns>An <see cref="IList"/> of <see cref="ProductName"/> data contract types.</returns>
        public IList<DC.ProductName> MapFromProductIdentifierListToProductNameList(IEnumerable<ProductIdentifier> productList)
        {
            var response = new GetProductsResponse();
            var productNames = from p in productList 
                               select new DC.ProductName { Id = p.Id, Name = p.Name };
            return productNames.ToList();
        }
    }
}
