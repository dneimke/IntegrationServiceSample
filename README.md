#IntegrationServiceSample

A .NET reference architecture for how to create a home-spun integration services layer.  

The goal is to provide a good enough reference for how to separate systems when building an integration hub within your enterprise.

The project demonstrates:

* Separation of concerns
* Mapping layers
* Highly maintainable and testable architecture

## Architecture

The architecture is that we have a system which is separated out into 3 primary domains

1.  Backend Line-of-Business (LOB) Applications - there are two of these, a Product Management application and a Loan Management application
2.  Integration Hub - A Web Service that provides a set of operations that allow the Frontend client application to integrate with the backend LOB applications
3.  Frontend Client Application - In the case of the sample, this is a simple console application, but think of it as a CRM or ERP application that your company has procured and which needs to integrate with back-end systems.