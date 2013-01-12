using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.IntegrationService.ProcessComponents.Loans;
using System.Collections.Generic;
using Company.LOB.ProductManagement.Entities;

namespace Company.IntegrationService.BusinessLayer.Tests.Unit
{
    [TestClass]
    public class GetProductsOperationMappingFixture
    {
        GetProductsProcessMappings mappings;

        [TestInitialize]
        public void Setup()
        {
            mappings = new GetProductsProcessMappings();
        }

        [TestMethod]
        public void ShouldMapFromProductIdentifierListToProductNameList()
        {
            // Arrange
            var request = new List<ProductIdentifier>
            {
                new ProductIdentifier { Id = 1, Name = "First" }
            };

            // Act
            var result = mappings.MapFromProductIdentifierListToProductNameList(request);
        
            // Assert
            Assert.AreEqual("First", result[0].Name);
            Assert.AreEqual(1, result[0].Id);
        }
    }
}
