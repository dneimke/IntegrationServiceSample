using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.Contracts.MessageContracts
{
    public class CompleteRequest
    {
        public bool ShouldSucceed;
        public Applicant Applicant;
    }
}
