using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.ProcessComponents;
using Company.IntegrationService.ProcessComponents.Loans;
using Company.LOB.LoanManagement.Client;
using Company.LOB.ProductManagement.Client;

namespace Company.IntegrationService
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

            container.Register<IProcessComponent<CompleteRequest, CompleteResponse>>(new CompleteProcess(loansClientProxy));
            container.Register<IProcessComponent<GetProductsRequest, GetProductsResponse>>(new GetProductsProcess(productsClientProxy));
            container.Register<IProcessComponent<IsEligibleRequest, IsEligibleResponse>>(new IsEligibleProcess(loansClientProxy));
            
            return container;
        }
    }
}
