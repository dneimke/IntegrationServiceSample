using System;
using System.Collections.Generic;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.ProcessComponents;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.Mappings.Loans
{

    public class CompleteRequestToCustomerLoansMap : IMappingFunction<CompleteRequest, CustomerLoans>
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
}
