using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.FrontEndSystems.LoanApplicationSystem.Examples
{
    internal abstract class Application
    {
        public string CustomerName { get; protected set; }
        public string ProductName { get; protected set; }

    }
}
