﻿using System;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Client;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class CompleteProcess : IProcessComponent<CompleteRequest, CompleteResponse>
    {
        private readonly ILoansClientProxy loansClient = null;
        private readonly CompleteProcessMappings mappings;

        public CompleteProcess(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
            mappings = new CompleteProcessMappings();
        }

        public CompleteResponse Process(CompleteRequest request)
        {
            var customerLoans = mappings.CreateCustomerLoansFromCompleteRequest(request);
            var result = loansClient.Upload(customerLoans);

            var response = mappings.CreateCompleteResponseFromInputs(request, result);
            return response;
        }
    }
}
