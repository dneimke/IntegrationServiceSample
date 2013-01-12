using System;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    // 1 component per operation
    public class CompleteProcess : IProcessComponent<CompleteRequest, CompleteResponse>
    {
        // Manages all of its own dependencies
        private readonly ILoansClientProxy loansClient = null;
        private readonly CompleteProcessMappings mappings;

        public CompleteProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
            mappings = new CompleteProcessMappings();
        }

        // The process operation runs all of the steps to process the request
        public CompleteResponse Process(CompleteRequest request)
        {
            var customerLoans = mappings.CreateCustomerLoansFromCompleteRequest(request);
            var result = loansClient.Upload(customerLoans);

            var response = mappings.PrepareResponseType(request, result);
            return response;
        }
    }
}
