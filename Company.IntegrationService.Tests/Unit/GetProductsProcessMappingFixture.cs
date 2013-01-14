using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.IntegrationService.ProcessComponents.Loans;
using System.Collections.Generic;
using Company.LOB.ProductManagement.Entities;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.Tests.Unit
{
    [TestClass]
    public class GetProductsProcessMappingFixture
    {
        [TestMethod]
        public void ShouldMapFromProductIdentifierListToProductNameList()
        {
            // Arrange
            var input = new List<ProductIdentifier>
            {
                new ProductIdentifier { Id = 1, Name = "First" }
            };

            // Act
            var result = new GetProductsOutput().Map(input);
            
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
