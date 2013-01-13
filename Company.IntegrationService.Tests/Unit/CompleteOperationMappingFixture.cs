﻿using Company.IntegrationService.Contracts.MessageContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.IntegrationService.ProcessComponents.Loans;

namespace Company.IntegrationService.Tests.Unit
{
    [TestClass]
    public class CompleteOperationMappingFixture
    {
        CompleteProcessMappings mappings;

        [TestInitialize]
        public void Setup()
        {
            mappings = new CompleteProcessMappings();
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
            var result = mappings.CompleteRequestToCustomerLoansMap.Map(request);
        
            // Assert
            Assert.AreEqual("Fred", result.Customer.Name);
        }
    }
}
