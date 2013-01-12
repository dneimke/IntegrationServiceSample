using System;
using System.Collections.Generic;
using System.Diagnostics;
using Company.LOB.ProductManagement.Entities;

namespace Company.LOB.ProductManagement.Client
{
    public interface IProductsClientProxy
    {
        ProductIdentifier GetByName(ProductName name);
        int ProductAvailability(string query);
        IEnumerable<ProductIdentifier> GetAllProducts();
    }

    public class ProductsClientProxy : IProductsClientProxy
    {
        public ProductsClientProxy() { }

        public ProductIdentifier GetByName(ProductName name)
        {
            Console.WriteLine("In the call: IProductsClientProxy.GetByName");
            return new ProductIdentifier { Id = 1, Name = name.Name };
        }

        public int ProductAvailability(string query)
        {
            Console.WriteLine("In the call: IProductsClientProxy.ProductAvailability");
            return query[0];
        }

        public IEnumerable<ProductIdentifier> GetAllProducts()
        {
            Console.WriteLine("In the call: IProductsClientProxy.GetAllProducts");

            var products = new ProductIdentifier[] { 
               new ProductIdentifier { Id = 1, Name = ProductName.Medium.Name },
               new ProductIdentifier { Id = 1, Name = ProductName.Happy.Name },
               new ProductIdentifier { Id = 1, Name = ProductName.Advanced.Name }
            };

            return products;
        }
    }
}
