using Company.IntegrationService.Contracts.MessageContracts;

namespace Company.IntegrationService.Contracts
{
    public interface ILoansIntegrationService
    {
        IsEligibleResponse IsEligible(IsEligibleRequest request);
        CompleteResponse Complete(CompleteRequest request);
        GetProductsResponse GetProducts(GetProductsRequest request);
    }
}
