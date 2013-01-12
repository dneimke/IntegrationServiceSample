using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Company.IntegrationService.ServiceContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.BusinessLayer.Components;
using Company.IntegrationService.BusinessLayer;

namespace Company.IntegrationService.WebServices
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
            var component = container.Resolve<IProcessRequestComponent<IsEligibleRequest, IsEligibleResponse>>();
            return component.Process(request);
        }

        public CompleteResponse Complete(CompleteRequest request)
        {
            var component = container.Resolve<IProcessRequestComponent<CompleteRequest, CompleteResponse>>();
            return component.Process(request);
        }

        public GetProductsResponse GetProducts(GetProductsRequest request)
        {
            var component = container.Resolve<IProcessRequestComponent<GetProductsRequest, GetProductsResponse>>();
            return component.Process(request);
        }
    }
}
