using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Contracts.DataContracts
{
    public class ProductNames
    {
        public static ProductName Happy = new ProductName("Standard");
        public static ProductName Medium = new ProductName("Medium");
        public static ProductName Advanced = new ProductName("Advanced");
    }

    public class ProductName
    {

        public string Name;

        private ProductName() { }

        public ProductName(string name)
        {
            this.Name = name;
        }
    }
}
