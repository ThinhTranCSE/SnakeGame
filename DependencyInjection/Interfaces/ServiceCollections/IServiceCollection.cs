using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.DIContainer;
using DependencyInjection.Patterns.Builder.Interfaces.Descriptor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Interfaces.ServiceCollections
{
    public interface IServiceCollection
    {
        Dictionary<Type, IServiceDescriptor> Descriptors { get; }

        IDescriptorBuilder StartRegister();
        IDIContainer BuildContainer();
    }

}
