#IntegrationServiceSample

A .NET reference architecture for creating a home-spun integration services layer with a of providing a good enough reference for how to separate systems when building an integration hub within your enterprise.

## TOC

* <a href="#overview">Overview</a>
* <a href="#architecture">Architecture</a>
* <a href="#service-operations">Service Operations</a>
* <a href="#data-contracts">Data Contracts</a>
* <a href="#mapping">Mapping</a></li>
* <a href="#integration-process-components">Integration Process Components</a>
* <a href="#testing-approach">Testing Approach</a>
* <a href="#fault-management">Fault Management</a>
* <a href="#dependency-management">Dependency Management</a>

## Overview

The design goals of the Integration Service are to:

* Provide an interface which provides 'process integration' between CRM and existing LOB applications.  The purpose of this is to reduce the complexity of service interactions.
* Help reduce versioning conflicts by forcing all communication through schema and contracts which are designed solely for the purpose of integration and thus preventing tight coupling between individual systems.  Part of this would come under the 'Boundaries are Explicit' core tenet of SOA.
* Design the system in such a way that it is easy to test to help ensure that risks associated with change and release management can be minimized.


## Architecture

The architecture is separated out into 3 primary domains

1.  Line-of-Business (LOB) Applications - there are two of these, a Product Management application and a Loan Management application.
2.  CRM Application - Implemented a simple console application, which represents a system procured by your company and which needs to integrate with existing back-end LOB systems.
3.  Integration Service - A Web Service that allows the CRM client application to integrate with the LOB applications

![System dependencies](https://github.com/dneimke/IntegrationServiceSample/raw/master/DependencyDiagram.PNG)


## Service Operations

When designing an integration service, the first step is to clearly identify and to document all of the individual points where integration will take place.  This is usually achieved by sitting down with business analysts and the architects of the systems that you will be integrating and to discuss the nature of the interaction and to ultimately, design and agree on a set of data contracts to which the interaction will conform.

In our example, this might be initiated by an engineer on the CRM team saying that, when a user clicks a button on a user interface, they will need to make a call to get a list of products that are available so that they can populate them in a choice list on a dialog that they intend to show.

At this point, you have your first operation, let's call this operation 'GetProducts'.


## Data Contracts

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


The data contracts are then wrapped in message contracts which act as boundaries at which interception can take place to apply consistent policy and infrastructure rules on all incoming and outgoing communication.


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


## Mapping

Mappers provide a way to have communications between two subsystems that still need to stay ignorant of each other. [Fowler] [1] 

In the case of this application, the Mapping layer provides us with a way of separating the IntegrationService from any domain objects that are exposed through the BusinessLayer.  This is done by sharing the data and message contracts between the business layer and the Services layer.

The pattern for communication layer and the business layer is established via the IProcessComponent interface which takes a Message contract in, and returns a processed result as a Message contract.

In designing the communications protocol, data is traced from the request, through to the back end system, and then back out through the response - a complete data roundtrip so to speak.

![Mapping strategy](https://github.com/dneimke/IntegrationServiceSample/raw/master/Mapping.PNG)

At the integration boundaries, every piece of data must be uniquely mapped according to naming and data type and recorded in an appropriate mapping dictionary.

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

Only 1 mapping is defined for the operation shown because the initial call to the Product System does not take any parameter arguments.

Architecturally, each Mapping is implemented as a class which helps to make them easier to maintain and test.  The Mapping classes inherit from an abstract base class which handles the underlying infrastructure for wiring up the mapping:

    public abstract class MappingFunction<TIn, TOut>
    {
        private Func<TIn, TOut> _mapper;
        public Func<TIn, TOut> Mapper
        {
            private get
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

        public TOut Map(TIn input)
        {
            return Mapper(input);
        }

        protected abstract TOut Default(TIn input);
    }

This leaves only the specific mapping functionality to be defined in each specific mapping class.

    public class GetProductsOutput : MappingFunction<ProductIdentifier, ProductName>
    {
        protected override int Default(string input)
        {
            // mapping implementation abbreviated
        }
    }

The Mapping classes are organised withing a namespace and class name structure that is designed to make them easy to find and thus improves the overall maintainability of the system.

<pre>
  /Mappings
    + MappingFunction.cs 
    /ServiceName
      + CommonMappings.cs
      + ProcessNameMappings.cs
         - public class LookupOperationInput : MappingFunction&lt;TIn, TOut>
         - public class LookupOperationOutput : MappingFunction&lt;TOut, TIn>
</pre>

The class ProcessNameMappings would infer the name of the underlying process class that it provides mappings for - in the case of this example it would be GetProductsProcessMappings.cs.  This file contains a class for each mapping that is required by the process. 

Each Mappings class is then tested against agreed translation and processing rules.  In the case of the above mapping, it is a straight mapping from both name and data type - but there might be other filters, lookups, translations, agreed defaults, etc. that need to be catered for to correctly implement integration.

## Integration Process Components

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
            /* process steps abbreviated abbreviated */ 
        }
    }

This implementation class implements the correct signature of Request->Reponse types, it receives its dependencies via dependency injection, and it then processes the operation behind the Process method of the interface.

## Testing Approach

## Fault Management

All exceptions are passed up through the application as IntegrationException types.  There are 3 different sub-types of IntegrationException which describe specific exceptions, they are:

* ProcessException - Catch all exceptions types for exceptions which are handled within IProcessComponent control flow processes and which are not of either the MappingException or CommunicationException types 
* MappingException - Exceptions which are thrown from within custom Mapping classes - e.g. where a translation rule within a Mapping class cannot be run because a specific rule is violated
* CommunicationException - Exceptions which are thrown as a result of a failed interaction with a remote process or external system - e.g. a database call, filesystem operation, or web service invocation 

FaultExceptioons 

## Dependency Management





[1]: http://martinfowler.com/eaaCatalog/mapper.html        "Martin Fowler, P of EAA Catalog: Mapper"
