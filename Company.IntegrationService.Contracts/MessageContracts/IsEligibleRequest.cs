using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.Contracts.DataContracts;


namespace Company.IntegrationService.Contracts.MessageContracts
{
    public class IsEligibleRequest {
        public ProductName Product;
        public Applicant Applicant;
    }
}
