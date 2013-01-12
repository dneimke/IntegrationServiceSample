using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Contracts.DataContracts
{
    public class LoanApplication
    {
        public Applicant Applicant;
        public IEnumerable<ResultantLoan> ResultantLoans;

        public bool IsApproved;
        public string ApprovalMessage;
    }
}
