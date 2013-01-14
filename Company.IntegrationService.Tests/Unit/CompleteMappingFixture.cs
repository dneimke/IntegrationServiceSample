using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.IntegrationService.ProcessComponents.Loans;
using Company.IntegrationService.Mappings.Loans;
using Company.LOB.LoanManagement.Entities;

namespace Company.IntegrationService.Tests.Unit
{
    [TestClass]
    public class CompleteProcessMappingFixture
    {
        [TestMethod]
        public void ShouldMapFromCompleteRequestToCustomerLoans()
        {
            // Arrange
            var input = new CompleteRequest
            {
                Applicant = new Contracts.DataContracts.Applicant { Name = "Fred" },
                ShouldSucceed = true
            };

            // Act
            var result = new GetCustomerLoansInput().Map(input);
        
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ShouldMapFromInputsToCompleteRequest()
        {
            // Arrange
            var request = new CompleteRequest
            {
                Applicant = new Contracts.DataContracts.Applicant { Name = "Fred" },
                ShouldSucceed = true
            };

            var loanNumber = new LoanNumber
            {
                Value = 1
            };

            // Act
            var result = new GetCustomerLoansOutput().Map(request, loanNumber);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
