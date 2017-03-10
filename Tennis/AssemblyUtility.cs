using System;
using System.Collections.Generic;
using System.Linq;

namespace Tennis
{
    public class AssemblyUtility
    {
        public static IEnumerable<T> CreateInstancesFromAssemblyTypes<T>()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(T).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(x => (T) Activator.CreateInstance(x));
        }
    }
}