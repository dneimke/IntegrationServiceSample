using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class IsEligibleProcessMappings
    {
        public ApplicantToCustomerAccountMap ApplicantToCustomerAccountMap = new ApplicantToCustomerAccountMap();
        public BooleanToIsEligibleResponseMap BooleanToIsEligibleResponseMap = new BooleanToIsEligibleResponseMap();
    }
}
