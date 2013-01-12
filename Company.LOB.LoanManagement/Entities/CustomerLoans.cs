using System.Collections.Generic;

namespace Company.LOB.LoanManagement.Entities
{
    public class CustomerLoans
    {
        public CustomerAccount Customer;
        public IEnumerable<Loan> Loans; 
    }
}
