using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.Contracts.MessageContracts;
using DC = Company.IntegrationService.Contracts.DataContracts;
using Company.LOB.ProductManagement.Entities;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class GetProductsProcessMappings
    {
        public ProductIdentifierListToProductNameListMap ProductIdentifierListToProductNameListMap = new ProductIdentifierListToProductNameListMap();
    }
}
