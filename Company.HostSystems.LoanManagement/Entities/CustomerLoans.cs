using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.BackEndSystems.LoanManagement.Entities
{
    public class CustomerLoans
    {
        public CustomerAccount Customer;
        public IEnumerable<Loan> Loans; 
    }
}
