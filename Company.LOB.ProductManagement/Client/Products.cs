using System;
using System.Collections.Generic;
using System.Diagnostics;
using Company.LOB.ProductManagement.Entities;

namespace Company.LOB.ProductManagement.Client
{
    public interface IProductsClientProxy
    {
        Product GetByName(ProductName name);
        int ProductAvailability(string query);
        IEnumerable<Product> GetAllProducts(int someNumber, decimal someDecimal, char aCharacter);
    }

    public class ProductsClientProxy : IProductsClientProxy
    {
        public ProductsClientProxy() { }

        public Product GetByName(ProductName name)
        {
            Console.WriteLine("In the call: IProductsClientProxy.GetByName");
            return new Product
            {
                Id = new ProductIdentifier { Id = 1, Name = name.Name },
                Name = new ProductName(name.Name)
            };
        }

        public int ProductAvailability(string query)
        {
            Console.WriteLine("In the call: IProductsClientProxy.ProductAvailability");
            return query[0];
        }

        public IEnumerable<Product> GetAllProducts(int someNumber, decimal someDecimal, char aCharacter)
        {
            Console.WriteLine("In the call: IProductsClientProxy.GetAllProducts");
            
            var products = new Product[] { 
                new Product
                {
                    Id = new ProductIdentifier { Id = 1, Name = ProductName.Medium.Name },
                    Name = new ProductName(ProductName.Medium.Name)
                },
                    new Product
                {
                    Id = new ProductIdentifier { Id = 1, Name = ProductName.Happy.Name },
                    Name = new ProductName(ProductName.Happy.Name)
                },
                    new Product
                {
                    Id = new ProductIdentifier { Id = 1, Name = ProductName.Advanced.Name },
                    Name = new ProductName(ProductName.Advanced.Name)
                }
            };

            return products;
        }
    }
}
