using System;
using Company.BackEndSystems.LoanManagement.Client;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.BackEndSystems.LoanManagement.Entities;
using System.Collections.Generic;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.BusinessLayer.Components.LoansService
{
    // 1 component per operation
    public class CompleteProcessRequestComponent : IProcessRequestComponent<CompleteRequest, CompleteResponse>
    {
        // Manages all of its own dependencies
        private readonly ILoansClientProxy loansClient = null;
        private readonly CompleteProcessRequestComponentMappings mappings;

        public CompleteProcessRequestComponent(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
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
