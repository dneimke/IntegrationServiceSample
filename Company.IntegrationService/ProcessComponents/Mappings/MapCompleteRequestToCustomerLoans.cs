using System;
using System.Collections.Generic;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.ProcessComponents.Mappings
{

    public interface IMappingFunction<TResult> {  }

    public interface IMappingFunction<T1, TResult> : IMappingFunction<TResult> {
        Func<T1, TResult> Mapper { get; set; }
        TResult Map(T1 arg1);
    }

    public interface IMappingFunction<T1, T2, TResult> : IMappingFunction<TResult> {
        Func<T1, T2, TResult> Mapper { get; set; }
        TResult Map(T1 arg1, T2 arg2);
    }

    public interface IMappingFunction<T1, T2, T3, TResult> : IMappingFunction<TResult> {
        Func<T1, T2, T3, TResult> Mapper { get; set; }
        TResult Map(T1 arg1, T2 arg2, T3 arg3);
    }

    // A class which allows the mapper to be injected - this might be a mapper which, for example,
    // has to manage complex, external operations
    public class MapCompleteRequestToCustomerLoans : IMappingFunction<CompleteRequest, CustomerLoans>
    {
        private Func<CompleteRequest, CustomerLoans> _mapper;
        public Func<CompleteRequest, CustomerLoans> Mapper
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

        public CustomerLoans Map(CompleteRequest request)
        {
            return this.Mapper(request);
        }


        private CustomerLoans Default(CompleteRequest request)
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
    }


    // A class which allows the mapper to be injected - this might be a mapper which, for example,
    // has to manage complex, external operations
    public class MapCompleteResponseFromInputs : IMappingFunction<CompleteRequest, LoanNumber, CompleteResponse>
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
