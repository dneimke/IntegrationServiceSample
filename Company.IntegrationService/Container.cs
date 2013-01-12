using System;
using System.Collections.Generic;

namespace Company.IntegrationService
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
