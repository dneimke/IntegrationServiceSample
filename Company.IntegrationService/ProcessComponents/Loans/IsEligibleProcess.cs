using System;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;


namespace Company.IntegrationService.ProcessComponents.Loans
{
    // 1 component per operation
    public class IsEligibleProcess : IProcessComponent<IsEligibleRequest, IsEligibleResponse>
    {
        // Manages all of its own dependencies
        private readonly ILoansClientProxy loansClient = null;
        private readonly IsEligibleProcessMapping mappings;

        public IsEligibleProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
            mappings = new IsEligibleProcessMapping();
        }

        // The process operation runs all of the steps to process the request
        public IsEligibleResponse Process(IsEligibleRequest request)
        {
            var customerAccount = mappings.MapFromApplicantToCustomerAccount(request.Applicant);
            var result = loansClient.IsEligible(request.Product.Name, customerAccount);
            return mappings.MapFromDomainResultToResponseType(result);
        }
    }
}
