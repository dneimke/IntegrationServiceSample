using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.BusinessLayer.Components
{
    public interface IProcessRequestComponent<TIn, TOut>
    {
        TOut Process(TIn request);
    }
}
