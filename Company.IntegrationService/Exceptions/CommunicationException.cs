using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Exceptions
{
    /// <summary>
    /// Exceptions which are thrown as a result of a failed interaction with a remote process or external 
    /// system - e.g. a database call, filesystem operation, or web service invocation
    /// </summary>
    public class CommunicationException : IntegrationException
    {
    }
}
