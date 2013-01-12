using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.Contracts.MessageContracts;

namespace Company.IntegrationService.ServiceContracts
{
    public interface ILoansIntegrationService
    {
        IsEligibleResponse IsEligible(IsEligibleRequest request);
        CompleteResponse Complete(CompleteRequest request);
        GetProductsResponse GetProducts(GetProductsRequest request);
    }
}
