#IntegrationServiceSample

A .NET reference architecture for how to create a home-spun integration services layer.  

The goal is to provide a good enough reference for how to separate systems when building an integration hub within your enterprise.

The project demonstrates:

* Separation of concerns
* Mapping layers
* Highly maintainable and testable architecture

## Architecture

The architecture is separated out into 3 primary domains

1.  Line-of-Business (LOB) Applications - there are two of these, a Product Management application and a Loan Management application.
2.  CRM Application - Implemented a simple console application, which represents a system procured by your company and which needs to integrate with existing back-end LOB systems.
3.  Integration Service - A Web Service that allows the CRM client application to integrate with the LOB applications

![System dependencies](https://github.com/dneimke/IntegrationServiceSample/raw/master/DependencyDiagram.PNG)

## Design Goals

The design goals of the Integration Service are to:

* Provide an interface which provides 'process integration' between CRM and existing LOB applications.  The purpose of this is to reduce the complexity of service interactions.
* Help reduce versioning conflicts by forcing all communication through schema and contracts which are designed solely for the purpose of integration and thus preventing tight coupling between individual systems.  Part of this would come under the 'Boundaries are Explicit' core tenet of SOA.
* Design the system in such a way that it is easy to test to help ensure that risks associated with change and release management can be minimized.

## How Service Operations are formed

When designing an integration service, the first step is to clearly identify and to document all of the individual points where integration will take place.  This is usually achieved by sitting down with business analysts and the architects of the systems that you will be integrating and to discuss the nature of the interaction and to ultimately, design and agree on a set of data contracts to which the interaction will conform.

In our example, this might be initiated by an engineer on the CRM team saying that, when a user clicks a button on a user interface, they will need to make a call to get a list of products that are available so that they can populate them in a choice list on a dialog that they intend to show.

At this point, you have your first operation, let's call this operation 'GetProducts'.


## Designing and Implementing Data Contracts

Agreements must be made about the data to be exchanged in order to to fulfil the requirements of the request  from the CRM system.  In the sample, it has been agreed that the request from the CRM system can specify a filter clause to specify what types of records it wants returned.  

    public enum ProductFilterClause
    {
        All,
        Some,
        None
    }

An agreement is also reached about the definition of data which represents each product in the result list.  In this case it is agreed that a data structure will be returned for each product which contains the product Id in the products system and also its name.

    public class ProductName
    {
        public int Id;
        public string Name;
    }


The data contracts are then wrapped in message contracts which act as boundaries at which we interception can take place to apply consistent policy and infrastructure rules on all incoming and outgoing communication.


After wrapping the data contracts in messages, the following contracts serve as the agreed communication protocol for requesting products.

    public class GetProductsRequest
    {
        public ProductQuery Filter;
    }


    public class GetProductsResponse
    {
        public List<ProductName> ProductNames;
    }

In its complete form, this is be represented by the following contract specifcation for the GetProducts operation:

    
    GetProductsResponse GetProducts(GetProductsRequest request);


## Mapping Data between systems

Mappers provide a way to have communications between two subsystems that still need to stay ignorant of each other. [Fowler] [1] 

In the case of this appilication, the Mapping layer provides the mapping that allows us to separate the IntegrationService from any domain objects that are exposed through the BusinessLayer.  This is done by sharing the data and message contracts between the business layer and the Services layer.

The pattern for communication layer and the business layer is established via the IProcessRequestComponent interface which takes a Message contract in, and returns a processed result as a Message contract.

In designing the communications protocol, the next step is to trace the data from the request, through to the back end system, and then back out through the response - a complete data roundtrip so to speak.

![Mapping strategy](https://github.com/dneimke/IntegrationServiceSample/raw/master/Mapping.PNG)

As shown in the diagram above, at the integration boundaries, every piece of data must be uniquely mapped according to naming and data type.  These should be developed and maintained as mapping tables in a spreadsheet or some such record.

> ### Mapping dictionary for GetProducts
> **Operation Name**: GetProducts
> **Operation Step**: Response from call to GetProducts method
> 
> **Source system**: LOB.Products 
> **Target system**: IntegrationService
> 
> **Source data type**: Company.LOB.ProductManagement.Entities.ProductIdentifier
> **Target data type**: Company.IntegrationService.Contracts.DataContracts.ProductName
>  
> <table>
>   <tr>
>     <th>Source Member</th> <th>Source Type</th> <th>Target Member</th> <th>Target Type</th> <th>Processing Rules</th>
>   </tr>
>   <tr>
>     <td>Id</td> <td>int</td> <td>Id</td> <td>int</td> <td>None</td>
>   </tr>
>   <tr>
>     <td>Name</td> <td>string</td> <td>Name</td> <td>string</td> <td>None</td>
>   </tr>
> </table>

Only 1 mapping is defined for the operation because the initial call to the Product System does not take any parameter arguments.

Architecturally, each set of mappings is implemented as a class which helps to make them easier to maintain and test.

    public class GetProductsProcessMappings
    {
        /// <summary>
        /// Maps from an <see cref="IEnumerable"/> of Product Management <see cref="ProductIdentifier"/> 
        /// to an <see cref="IList"/> of <see cref="ProductName"/> items.
        /// </summary>
        /// <param name="productList">An <see cref="IEnumerable"/> of Product Management 
        /// <see cref="ProductIdentifier"/> items to map from
        /// </param>
        /// <returns>An <see cref="IList"/> of <see cref="ProductName"/> data contract types.</returns>
        public IList<DC.ProductName> MapFromProductIdentifierListToProductNameList(IEnumerable<ProductIdentifier> productList)
        {
            var response = new GetProductsResponse();
            var productNames = from p in productList 
                               select new DC.ProductName { Id = p.Id, Name = p.Name };
            return productNames.ToList();
        }
    }

In the case of the above mapping, it is a straight mapping from both name and data type - but there might be other filters, lookups, translations, agreed defaults, etc. that need to be catered for to correctly implement integration.

Each Mappings classes is then be tested against agreed translation rules and any other agreed rules.  

In the reference sample, there is one test Fixture for each Mappings class, but this may be further refined to one Fixture per mapping rule.

    [TestClass]
    public class GetProductsOperationMappingFixture
    {
        GetProductsProcessMappings mappings;

        [TestInitialize]
        public void Setup()
        {
            mappings = new GetProductsProcessMappings();
        }

        [TestMethod]
        public void ShouldMapFromProductIdentifierListToProductNameList()
        {
            // Arrange
            var request = new List<ProductIdentifier>
            {
                new ProductIdentifier { Id = 1, Name = "First" }
            };

            // Act
            var result = mappings.MapFromProductIdentifierListToProductNameList(request);
        
            // Assert
            Assert.AreEqual("First", result[0].Name);
            Assert.AreEqual(1, result[0].Id);
        }
    }


## Designing and Implementing Integration Processes

Each individual integration process is implemented as a class which helps to make them easier to maintain and test.  The responsibilities of an integration process class are:

* It must only expose Data/Message contracts at its public boundary
* It should maintain a reference to all of the dependencies that it requires and it should receive those dependencies via dependency injection
* It is responsible for orchestration of the steps that are required to carry out the process in their correct order

Integration process classes are governed by the IProcessComponent which helps to enforce these constraints as it is the only visibility that the calling WebService should have over access to processes - e.g. creating of the processing class would ultimately be delegated to a Factory which looked at incoming requests and outgoing response message types and mapped them to an underlying implementation.

The IProcessComponent interface is as follows:

    public interface IProcessComponent<TIn, TOut>
    {
        TOut Process(TIn request);
    }

The following implementation of this interface exists for the GetProducts operation.

    public class GetProductsProcess : IProcessComponent<GetProductsRequest, GetProductsResponse>
    {
        private readonly IProductsClientProxy productsClient = null;
        private readonly GetProductsProcessMappings mappings;

        public GetProductsProcess(IProductsClientProxy products) 
		{ 
		    /* ctor implementation abbreviated */ 
		}

        public GetProductsResponse Process(GetProductsRequest request)
        {
            var response = new GetProductsResponse { ProductNames = new List<ProductName>() };
            
            if (request.Filter == ProductFilterClause.None)
                return response;

            var productList = productsClient.GetAllProducts();

            var productNames = mappings.MapFromProductIdentifierListToProductNameList(productList);

            if (request.Filter == ProductFilterClause.Some)
            {
                // A silly business rule to highlight something we might do as a step...
                // randomly remove the first product name is ProductFilterClause.Some was selected
                productNames.Remove(productNames[0]); 
            }

            response.ProductNames = productNames;
            
            return response;
        }
    }

This implementation class implements the correct signature of Request->Reponse types, it receives its dependencies via dependency injection, and it then processes the operation behind the Process method of the interface.

## Dependency Management


[1]: http://martinfowler.com/eaaCatalog/mapper.html        "Martin Fowler, P of EAA Catalog: Mapper"
