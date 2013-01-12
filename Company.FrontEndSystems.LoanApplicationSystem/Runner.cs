using System;
using Company.FrontEndSystems.LoanApplicationSystem.Examples;
using Company.IntegrationService.Contracts.DataContracts;
using Company.IntegrationService.Contracts.MessageContracts;
using Company.IntegrationService.ServiceContracts;
using Company.IntegrationService.WebServices;

namespace Company.FrontEndSystems.LoanApplicationSystem
{
    class Runner
    {
        ILoansIntegrationService service;

        public Runner()
        {
            this.service = new LoansIntegrationServiceWithComponents();
        }


        internal void RunSimpleFailExample()
        {
            var application = new SimpleApplication();

            var isEligibleRequest = CreateSimpleEligibilityRequest(application);
            var response = service.IsEligible(isEligibleRequest);

            if (response.Eligible == true)
            {
                var completeRequest = CreateSimpleCompleteRequest(application, false);
                ProcessRequest(completeRequest);
            }
        }

        internal void RunSimpleSuccessExample()
        {
            var application = new SimpleApplication();

            var isEligibleRequest = CreateSimpleEligibilityRequest(application);
            var response = service.IsEligible(isEligibleRequest);

            if (response.Eligible == true)
            {
                var completeRequest = CreateSimpleCompleteRequest(application, true);
                ProcessRequest(completeRequest);
            }
        }

        internal void RunGetProductsExample()
        {
            var request = new GetProductsRequest
            {
                RequestedProducts = ProductQuery.All
            };

            var response = service.GetProducts(request);

            foreach (var product in response.ProductNames)
            {
                Console.WriteLine(product);
            }
        }

        private void ProcessRequest(CompleteRequest completeRequest)
        {
            var result = service.Complete(completeRequest);
            Console.WriteLine("Result {0}", result.LoanApplication.IsApproved);
            Console.WriteLine("Reason: {0}", result.LoanApplication.ApprovalMessage);
        }

        private IsEligibleRequest CreateSimpleEligibilityRequest(Application application)
        {
            return new IsEligibleRequest
            {
                Applicant = new Applicant { Name = application.CustomerName },
                Product = new ProductName(application.ProductName)
            };
        }

        private CompleteRequest CreateSimpleCompleteRequest(Application application, bool shouldSucceed)
        {
            return new CompleteRequest
            {
                ShouldSucceed = shouldSucceed,
                Applicant = new Applicant { Name = application.CustomerName }
            };
        }


    }
}
