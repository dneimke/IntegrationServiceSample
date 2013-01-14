using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Exceptions
{
    /// <summary>
    /// Catch all exceptions types for exceptions which are handled within IProcessComponent control 
    /// flow processes and which are not of either the MappingException or CommunicationException types
    /// </summary>
    public class ProcessException : IntegrationException
    {
    }
}
