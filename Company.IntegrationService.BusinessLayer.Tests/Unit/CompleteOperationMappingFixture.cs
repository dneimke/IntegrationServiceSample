using Company.IntegrationService.BusinessLayer.Components.LoansService;
using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.IntegrationService.BusinessLayer.Tests.Unit
{
    [TestClass]
    public class CompleteOperationMappingFixture
    {
        CompleteProcessRequestComponentMappings mappings;

        [TestInitialize]
        public void Setup()
        {
            mappings = new CompleteProcessRequestComponentMappings();
        }

        [TestMethod]
        public void ShouldMapFromCompleteRequestToCustomerLoans()
        {
            // Arrange
            var request = new CompleteRequest
            {
                Applicant = new Contracts.DataContracts.Applicant { Name = "Fred" },
                ShouldSucceed = true
            };

            // Act
            var result = mappings.CreateCustomerLoansFromCompleteRequest(request);
        
            // Assert
            Assert.AreEqual("Fred", result.Customer.Name);
        }
    }
}
