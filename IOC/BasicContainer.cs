using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCommandModelling.IOC
{
    public class BasicContainer
    {
        private Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public static BasicContainer IOC = new BasicContainer();

        public void Register<I, T>(T instance)
        {
            if (!typeof(I).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException("The interface is not assignable from the given instance type.");
            }

            _instances.Add(typeof(I), instance);
        }

        public T Resolve<T>()
        {
            if (_instances.ContainsKey(typeof(T)))
            {
                return (T) _instances[typeof(T)];
            }

            return default(T);
        }

        public bool IsTypeRegistered<T>()
        {
            return _instances.ContainsKey(typeof(T));
        }
    }
}
