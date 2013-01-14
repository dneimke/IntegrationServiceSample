using System;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;
using Company.IntegrationService.Mappings.Loans;


namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class IsEligibleProcess : IProcessComponent<IsEligibleRequest, IsEligibleResponse>
    {
        private readonly ILoansClientProxy loansClient = null;
        
        public IsEligibleProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
        }

        public IsEligibleResponse Process(IsEligibleRequest request)
        {
            var customerAccount = new IsEligibleInput().Map(request.Applicant);
            var result = loansClient.IsEligible(request.Product.Name, customerAccount);
            return new IsEligibleOutput().Map(result);
        }
    }
}
