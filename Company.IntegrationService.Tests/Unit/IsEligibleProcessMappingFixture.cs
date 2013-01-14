using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.IntegrationService.ProcessComponents.Loans;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.Tests.Unit
{
    [TestClass]
    public class IsEligibleProcessMappingFixture
    {
        [TestMethod]
        public void ShouldMapFromApplicantToCustomerAccount()
        {
            // Arrange
            var input = new CompleteRequest
            {
                Applicant = new Contracts.DataContracts.Applicant { Name = "Fred" },
                ShouldSucceed = true
            };

            // Act
            var result = new IsEligibleInputMapper().Map(input.Applicant);
        
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ShouldCreateIsEligibleResponseFromBooleanResult()
        {
            // Act
            var result = new IsEligibleOutputMapper().Map(true);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
