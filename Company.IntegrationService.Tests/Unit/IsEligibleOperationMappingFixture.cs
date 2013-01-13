using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.IntegrationService.ProcessComponents.Loans;

namespace Company.IntegrationService.Tests.Unit
{
    [TestClass]
    public class IsEligibleOperationMappingFixture
    {
        IsEligibleProcessMappings mappings;

        [TestInitialize]
        public void Setup()
        {
            mappings = new IsEligibleProcessMappings();
        }

        [TestMethod]
        public void ShouldMapFromApplicantToCustomerAccount()
        {
            // Arrange
            var request = new CompleteRequest
            {
                Applicant = new Contracts.DataContracts.Applicant { Name = "Fred" },
                ShouldSucceed = true
            };

            // Act
            var result = mappings.MapFromApplicantToCustomerAccount(request.Applicant);
        
            // Assert
            Assert.AreEqual("Fred", result.Name);
        }


        [TestMethod]
        public void ShouldCreateIsEligibleResponseFromBooleanResult()
        {
            // Act
            var result = mappings.CreateIsEligibleResponse(true);

            // Assert
            Assert.AreEqual(true, result.Eligible);
        }
    }
}
