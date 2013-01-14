using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Exceptions
{
    /// <summary>
    /// Exceptions which are thrown from within custom Mapping classes - e.g. where a 
    /// translation rule within a Mapping class cannot be run because a specific rule is violated
    /// </summary>
    public class MappingException : IntegrationException
    {
    }
}
