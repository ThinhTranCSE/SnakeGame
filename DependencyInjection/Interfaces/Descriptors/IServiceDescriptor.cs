using DependencyInjection.Enums;
using DependencyInjection.Patterns.Composite.Interfaces.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Interfaces.Descriptors
{
    public interface IServiceDescriptor : IResolvable, IDependent
    {
        Type ServiceType { get; }
        ServiceLifetime Lifetime { get; }
        IDictionary<IEnumerable<Type>, ConstructorInfo> ConstructorDictionary { get; }
    }
}
