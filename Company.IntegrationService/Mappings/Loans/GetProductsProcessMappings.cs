using System.Collections.Generic;
using System.Linq;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.ProductManagement.Entities;
using DC = Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.Mappings.Loans
{
    /// <summary>
    /// Maps from an <see cref="IEnumerable"/> of Product Management <see cref="ProductIdentifier"/> 
    /// to an <see cref="IList"/> of <see cref="ProductName"/> items.
    /// </summary>
    /// <param name="productList">An <see cref="IEnumerable"/> of Product Management 
    /// <see cref="ProductIdentifier"/> items to map from
    /// </param>
    /// <returns>An <see cref="IList"/> of <see cref="ProductName"/> data contract types.</returns>
    public class GetProductsOutputMapper : MappingFunction<IEnumerable<ProductIdentifier>, IList<DC.ProductName>>
    {

        protected override IList<DC.ProductName> Default(IEnumerable<ProductIdentifier> input)
        {
            var productNames = from p in input
                               select new DC.ProductName { Id = p.Id, Name = p.Name };
            return productNames.ToList();
        }
    }
}
