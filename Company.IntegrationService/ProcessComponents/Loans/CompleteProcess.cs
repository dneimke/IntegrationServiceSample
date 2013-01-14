using System;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class CompleteProcess : IProcessComponent<CompleteRequest, CompleteResponse>
    {
        private readonly ILoansClientProxy loansClient = null;
        
        public CompleteProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
        }

        public CompleteResponse Process(CompleteRequest request)
        {
            var customerLoans = new GetCustomerLoansInput().Map(request);
            var result = loansClient.Upload(customerLoans);

            var response = new GetCustomerLoansOutput().Map(request, result);
            return response;
        }
    }
}
