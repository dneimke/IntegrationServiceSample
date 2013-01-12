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

## Design Goals

The design goals of the Integration Service are to:

* Provide an interface which provides 'process integration' between CRM and existing LOB applications.  The purpose of this is to reduce the complexity of service interactions.
* Help reduce versioning conflicts by forcing all communication through schema and contracts which are designed solely for the purpose of integration and thus preventing tight coupling between individual systems.  Part of this would come under the 'Boundaries are Explicit' core tenet of SOA.
* Design the system in such a way that it is easy to test to help ensure that risks associated with change and release management can be minimized.

## Designing and Implementing Data Contracts

When designing an integration service, the first step is to clearly identify and to document all of the individual points where integration will take place.  This is usually achieved by sitting down with business analysts and the architects of the systems that you will be integrating and to discuss the nature of the interaction and to ultimately, design and agree on a set of data contracts to which the interaction will conform.

In our example, this might be initiated by an engineer on the CRM team saying that, when a user clicks a button on a user interface, they will need to make a call to get a list of products that are available so that they can populate them in a choice list on a dialog that they intend to show.

Good, we have our first operation, let's call this operation 'GetProducts'.

    GetProductsResponse GetProducts(GetProductsRequest request);

Agreements must also be made about the data to be exchanged in order to to fulfil the requirements of the request  from the CRM system.  In the sample, it has been agreed that the request from the CRM system can specify a filter clause to specify what types of records it wants returned.  

    public enum ProductFilterClause
    {
        All,
        Some,
        None
    }

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

## Mapping Data between systems


## Designing and Implementing Integration Processes



