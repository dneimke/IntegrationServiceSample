using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.ProcessComponents;
using Company.IntegrationService.Contracts.MessageContracts;

namespace Company.IntegrationService.Mappings.Loans
{
    public class BooleanToIsEligibleResponseMap : IMappingFunction<bool, IsEligibleResponse>
    {
        private Func<bool, IsEligibleResponse> _mapper;
        public Func<bool, IsEligibleResponse> Mapper
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

        public IsEligibleResponse Map(bool isEligible)
        {
            return this.Mapper(isEligible);
        }


        private IsEligibleResponse Default(bool isEligible)
        {
            return new IsEligibleResponse { Eligible = isEligible };
        }
    }
}
