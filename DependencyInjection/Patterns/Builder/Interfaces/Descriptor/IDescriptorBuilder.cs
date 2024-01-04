using DependencyInjection.Enums;
using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Patterns.Composite.Interfaces.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DependencyInjection.Interfaces.Descriptors.IServiceDescriptor;

namespace DependencyInjection.Patterns.Builder.Interfaces.Descriptor
{
    public interface IDescriptorBuilder
    {
        Type ServiceType { get; }
        Type ImplementationType { get; }
        ServiceLifetime Lifetime { get; }
        IDescriptorBuilder RegisterType(Type ImplementationType);
        IDescriptorBuilder RegisterType<TImplementation>();
        IDescriptorBuilder As<TInterface>();
        IDescriptorBuilder As(Type InterfaceType);
        IDescriptorBuilder WithLifetime(ServiceLifetime Lifetime);
        IDescriptorBuilder Singleton();
        IDescriptorBuilder Transient();
        IServiceDescriptor EndRegister();
    }
}
