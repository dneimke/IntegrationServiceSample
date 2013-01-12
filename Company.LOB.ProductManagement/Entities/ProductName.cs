﻿
namespace Company.LOB.ProductManagement.Entities
{
    public class ProductName
    {
        public string Name;

        public ProductName(string name)
        {
            this.Name = name;
        }

        public static ProductName Happy = new ProductName("Standard");
        public static ProductName Medium = new ProductName("Medium");
        public static ProductName Advanced = new ProductName("Advanced");

        
    }
}
