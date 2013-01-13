using System;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;


namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class IsEligibleProcess : IProcessComponent<IsEligibleRequest, IsEligibleResponse>
    {
        private readonly ILoansClientProxy loansClient = null;
        private readonly IsEligibleProcessMappings mappings;

        public IsEligibleProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
            mappings = new IsEligibleProcessMappings();
        }

        public IsEligibleResponse Process(IsEligibleRequest request)
        {
            var customerAccount = mappings.MapFromApplicantToCustomerAccount(request.Applicant);
            var result = loansClient.IsEligible(request.Product.Name, customerAccount);
            return mappings.CreateIsEligibleResponse(result);
        }
    }
}
