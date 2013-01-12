using Company.IntegrationService.Contracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.ProcessComponents;


namespace Company.IntegrationService.WebService
{
    public class LoansIntegrationServiceWithComponents : ILoansIntegrationService
    {
        private Container container;

        public LoansIntegrationServiceWithComponents()
        {
            this.container = ApplicationBootstrapper.Bootstrap();
        }

        public IsEligibleResponse IsEligible(IsEligibleRequest request)
        {
            var component = container.Resolve<IProcessComponent<IsEligibleRequest, IsEligibleResponse>>();
            return component.Process(request);
        }

        public CompleteResponse Complete(CompleteRequest request)
        {
            var component = container.Resolve<IProcessComponent<CompleteRequest, CompleteResponse>>();
            return component.Process(request);
        }

        public GetProductsResponse GetProducts(GetProductsRequest request)
        {
            var component = container.Resolve<IProcessComponent<GetProductsRequest, GetProductsResponse>>();
            return component.Process(request);
        }
    }
}
