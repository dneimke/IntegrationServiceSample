using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class IsEligibleProcessMapping
    {
        public CustomerAccount MapFromApplicantToCustomerAccount(Applicant applicant)
        {
            return new CustomerAccount { Name = applicant.Name };
        }

        public IsEligibleResponse MapFromDomainResultToResponseType(bool result)
        {
            return new IsEligibleResponse { Eligible = result };
        }
    }
}
