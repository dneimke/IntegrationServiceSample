using System;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class CompleteProcess : IProcessComponent<CompleteRequest, CompleteResponse>
    {
        private readonly ILoansClientProxy loansClient = null;
        private readonly CompleteProcessMappings mappings = new CompleteProcessMappings();

        public CompleteProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
        }

        public CompleteResponse Process(CompleteRequest request)
        {
            var customerLoans = mappings.CompleteRequestToCustomerLoansMap.Map(request);
            var result = loansClient.Upload(customerLoans);

            var response = mappings.CompleteResponseFromInputsMap.Map(request, result);
            return response;
        }
    }
}
