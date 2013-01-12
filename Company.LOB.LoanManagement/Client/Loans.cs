using System;
using Company.LOB.LoanManagement.Entities;


namespace Company.LOB.LoanManagement.Client
{
    public interface ILoansClientProxy
    {
        bool IsEligible(string productName, CustomerAccount customer);
        LoanNumber Upload(CustomerLoans loans);
    }

    public class LoansClientProxy : ILoansClientProxy
    {
        public LoansClientProxy() {}

        public bool IsEligible(string productName, CustomerAccount customer)
        {
            Console.WriteLine("In the call: ILoansClientProxy.IsEligible");
            return customer.Name[0].ToString().StartsWith(productName[0].ToString());
        }


        public LoanNumber Upload(CustomerLoans loans)
        {
            Console.WriteLine("In the call: ILoansClientProxy.Upload");
            return new LoanNumber { Value = 1 };
        }
    }
}
