using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Contracts.DataContracts
{
    public class ResultantLoan
    {
        public ProductName Product;
        public decimal InterestRate;
        public decimal LoanAmount;
    }
}
