using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.ProcessComponents;

namespace Company.IntegrationService.Mappings.Loans
{
    // A class which allows the mapper to be injected - this might be a mapper which, for example,
    // has to manage complex, external operations
    public class CompleteResponseFromInputsMap : IMappingFunction<CompleteRequest, LoanNumber, CompleteResponse>
    {
        Func<CompleteRequest, LoanNumber, CompleteResponse> _mapper;
        public Func<CompleteRequest, LoanNumber, CompleteResponse> Mapper
        {
            get
            {
                if (this._mapper == null)
                    return this.Default;

                return _mapper;
            }
            set
            {
                this._mapper = value;
            }
        }

        public CompleteResponse Map(CompleteRequest arg1, LoanNumber arg2)
        {
            return this.Mapper(arg1, arg2);
        }


        private CompleteResponse Default(CompleteRequest request, LoanNumber loanNumber)
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
