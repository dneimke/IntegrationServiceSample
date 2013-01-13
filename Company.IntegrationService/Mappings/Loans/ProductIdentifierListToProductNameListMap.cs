using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.ProcessComponents;
using Company.LOB.ProductManagement.Entities;
using DC = Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.Mappings.Loans
{
    /// <summary>
    /// Maps from an <see cref="IEnumerable"/> of Product Management <see cref="ProductIdentifier"/> 
    /// to an <see cref="IList"/> of <see cref="ProductName"/> items.
    /// </summary>
    public class ProductIdentifierListToProductNameListMap : IMappingFunction<IEnumerable<ProductIdentifier>, IList<DC.ProductName>>
    {
        private Func<IEnumerable<ProductIdentifier>, IList<DC.ProductName>> _mapper;
        public Func<IEnumerable<ProductIdentifier>, IList<DC.ProductName>> Mapper
        {
            get
            {
                if (this._mapper == null)
                    return this.Default;

                return _mapper;
            }
            set
            {
                this._mapper = value;
            }
        }

        public IList<DC.ProductName> Map(IEnumerable<ProductIdentifier> request)
        {
            return this.Mapper(request);
        }


        private IList<DC.ProductName> Default(IEnumerable<ProductIdentifier> productList)
        {
            var productNames = from p in productList
                               select new DC.ProductName { Id = p.Id, Name = p.Name };
            return productNames.ToList();
        }
    }
}
