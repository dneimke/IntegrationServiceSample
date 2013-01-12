using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.BusinessLayer
{
    public class Container
    {
        private Dictionary<Type, object> dictionary = new Dictionary<Type, object>();

        public void Register<T>(object instance)
        {
            dictionary[typeof(T)] = instance;
        }

        public T Resolve<T>()
        {
            var obj = (T)dictionary[typeof(T)];
            return obj;
        }
    }
}
