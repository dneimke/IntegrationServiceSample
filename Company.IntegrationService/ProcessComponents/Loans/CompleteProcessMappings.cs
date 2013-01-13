using System.Collections.Generic;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Mappings.Loans;

namespace Company.IntegrationService.ProcessComponents.Loans
{
    public class CompleteProcessMappings
    {
        //public CompleteRequestToCustomerLoansMap CompleteRequestToCustomerLoansMap = new CompleteRequestToCustomerLoansMap();
        //public CompleteResponseFromInputsMap CompleteResponseFromInputsMap = new CompleteResponseFromInputsMap();

        /// <summary>
        /// Creates a new CustomerLoans instance with values mapped from an incoming CompleteRequest
        /// </summary>
        /// <param name="request">The CompleteRequest input value</param>
        /// <returns>A valid CustomerLoans instance</returns>
        public CustomerLoans CreateCustomerLoansFromCompleteRequest(CompleteRequest request)
        {
            return new CustomerLoans
            {
                Customer = new CustomerAccount { Name = request.Applicant.Name, Id = 1 },
                Loans = new List<Loan>
                {
                    new Loan
                    {
                        Id = 1,
                        LoanType = LoanType.A
                    }
                }
            };
        }


        /// <summary>
        /// Prepares the response message based on operations and inputs
        /// </summary>
        /// <param name="request">The initial request</param>
        /// <param name="loanNumber">A loan number domain entity from the LoanManagement domain</param>
        /// <returns></returns>
        public CompleteResponse CreateCompleteResponseFromInputs(CompleteRequest request, LoanNumber loanNumber)
        {
            return new CompleteResponse
            {
                LoanApplication = new LoanApplication
                {
                    Applicant = new Applicant { Name = request.Applicant.Name },
                    IsApproved = request.ShouldSucceed,
                    ApprovalMessage = (request.ShouldSucceed) ? string.Format("Loan Number is {0}", loanNumber.Value) : "Should not succeed based on input parameters"
                }
            };
        }
    }
}
