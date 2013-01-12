using System;
using Company.BackEndSystems.LoanManagement.Client;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.BackEndSystems.LoanManagement.Entities;
using System.Collections.Generic;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.BusinessLayer.Components.LoansService
{
    // 1 component per operation
    public class CompleteProcessRequestComponent : IProcessRequestComponent<CompleteRequest, CompleteResponse>
    {
        // Manages all of its own dependencies
        private readonly ILoansClientProxy loansClient = null;

        public CompleteProcessRequestComponent(ILoansClientProxy loansClient)
        {
            if (loansClient == null)
                throw new ArgumentNullException("loansClient");

            this.loansClient = loansClient;
        }

        // The process operation runs all of the steps to process the request
        public CompleteResponse Process(CompleteRequest request)
        {
            var customerLoans = CompleteOperationMapping.CreateCustomerLoansFromCompleteRequest(request);
            var result = loansClient.Upload(customerLoans);

            var response = CompleteOperationMapping.PrepareResponseType(request, result);
            return response;
        }


        #region Everything here is the logic for the steps that are carried out by this process component


        #endregion


        #region Mappings for this process
        private static class CompleteOperationMapping
        {
            /// <summary>
            /// Creates a new CustomerLoans instance with values mapped from an incoming CompleteRequest
            /// </summary>
            /// <param name="request">The CompleteRequest input value</param>
            /// <returns>A valid CustomerLoans instance</returns>
            internal static CustomerLoans CreateCustomerLoansFromCompleteRequest(CompleteRequest request)
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
            internal static CompleteResponse PrepareResponseType(CompleteRequest request, LoanNumber loanNumber)
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
        #endregion
    }
}
