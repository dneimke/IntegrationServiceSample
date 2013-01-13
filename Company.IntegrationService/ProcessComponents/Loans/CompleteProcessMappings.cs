using System.Collections.Generic;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class CompleteProcessMappings
    {
        public CompleteRequestToCustomerLoansMap CompleteRequestToCustomerLoansMap = new CompleteRequestToCustomerLoansMap();
        public CompleteResponseFromInputsMap CompleteResponseFromInputsMap = new CompleteResponseFromInputsMap();
    }
}
