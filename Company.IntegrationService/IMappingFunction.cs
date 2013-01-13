using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.ProcessComponents
{
    public interface IMappingFunction<TResult> {  }

    public interface IMappingFunction<T1, TResult> : IMappingFunction<TResult> {
        Func<T1, TResult> Mapper { get; set; }
        TResult Map(T1 arg1);
    }

    public interface IMappingFunction<T1, T2, TResult> : IMappingFunction<TResult> {
        Func<T1, T2, TResult> Mapper { get; set; }
        TResult Map(T1 arg1, T2 arg2);
    }

    public interface IMappingFunction<T1, T2, T3, TResult> : IMappingFunction<TResult> {
        Func<T1, T2, T3, TResult> Mapper { get; set; }
        TResult Map(T1 arg1, T2 arg2, T3 arg3);
    }
}
