using Company.BackEndSystems.LoanManagement.Client;
using Company.BackEndSystems.ProductManagement.Client;
using Company.IntegrationService.BusinessLayer.Components;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.BusinessLayer.Components.LoansService;

namespace Company.IntegrationService.BusinessLayer
{
    public class ApplicationBootstrapper
    {
        
        public static Container Bootstrap()
        {
            var container = new Container();

            var productsClientProxy = new ProductsClientProxy();
            var loansClientProxy = new LoansClientProxy();
            
            container.Register<IProductsClientProxy>(productsClientProxy);
            container.Register<ILoansClientProxy>(loansClientProxy);

            container.Register<IProcessRequestComponent<CompleteRequest, CompleteResponse>>(new CompleteProcessRequestComponent(loansClientProxy));
            container.Register<IProcessRequestComponent<GetProductsRequest, GetProductsResponse>>(new GetProductsProcessRequestComponent(productsClientProxy));
            container.Register<IProcessRequestComponent<IsEligibleRequest, IsEligibleResponse>>(new IsEligibleProcessRequestComponent(loansClientProxy));
            
            return container;
        }
    }
}
