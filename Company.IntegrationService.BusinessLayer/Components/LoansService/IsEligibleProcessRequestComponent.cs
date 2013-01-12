using System;
using Company.BackEndSystems.LoanManagement.Client;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.BackEndSystems.LoanManagement.Entities;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.BusinessLayer.Components.LoansService
{
    // 1 component per operation
    public class IsEligibleProcessRequestComponent : IProcessRequestComponent<IsEligibleRequest, IsEligibleResponse>
    {
        // Manages all of its own dependencies
        private readonly ILoansClientProxy loansClient = null;

        public IsEligibleProcessRequestComponent(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
        }

        // The process operation runs all of the steps to process the request
        public IsEligibleResponse Process(IsEligibleRequest request)
        {
            var customerAccount = IsEligibleOperationMapping.MapFromApplicantToCustomerAccount(request.Applicant);
            var result = loansClient.IsEligible(request.Product.Name, customerAccount);
            return IsEligibleOperationMapping.MapFromDomainResultToResponseType(result);
        }


        #region Everything here is the logic for the steps that are carried out by this process component
        

        #endregion


        #region Mappings for this process
        private class IsEligibleOperationMapping
        {
            internal static CustomerAccount MapFromApplicantToCustomerAccount(Applicant applicant)
            {
                return new CustomerAccount { Name = applicant.Name };
            }

            internal static IsEligibleResponse MapFromDomainResultToResponseType(bool result)
            {
                return new IsEligibleResponse { Eligible = result };
            }
        }
        #endregion
    }
}
