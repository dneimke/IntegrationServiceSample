using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.ProcessComponents
{
    public interface IProcessComponent<TIn, TOut>
    {
        TOut Process(TIn request);
    }
}
