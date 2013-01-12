using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Contracts.DataContracts
{
    public class ProductName
    {
        public int Id;
        public string Name;

        public ProductName() { }

        public ProductName(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
