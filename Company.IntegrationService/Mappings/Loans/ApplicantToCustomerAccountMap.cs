using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.ProcessComponents;
using Company.LOB.LoanManagement.Entities;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.Contracts.DataContracts;

namespace Company.IntegrationService.Mappings.Loans
{

    public class ApplicantToCustomerAccountMap : IMappingFunction<Applicant, CustomerAccount>
    {
        private Func<Applicant, CustomerAccount> _mapper;
        public Func<Applicant, CustomerAccount> Mapper
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

        public CustomerAccount Map(Applicant applicant)
        {
            return this.Mapper(applicant);
        }


        private CustomerAccount Default(Applicant applicant)
        {
            return new CustomerAccount { Name = applicant.Name };
        }
    }

    
}
