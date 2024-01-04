using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.DIContainer;
using DependencyInjection.Interfaces.ServiceCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Implementations.DIContainers
{
    public class DIContainer : IDIContainer
    {
        public Dictionary<Type, IServiceDescriptor> Descriptors { get; private set; }

        public DIContainer(IServiceCollection Services)
        {
            Descriptors = Services.Descriptors;
        }
        public TInterface Resolve<TInterface>(params object[] Args) where TInterface : class
        {
            return (TInterface)Resolve(typeof(TInterface), Args);
        }

        public object Resolve(Type TInterface, params object[] Args)
        {
            if(!Descriptors.ContainsKey(TInterface))
            {
                throw new Exception($"No service for type {TInterface.Name} has been registered");
            }

            IServiceDescriptor Descriptor = Descriptors[TInterface];
            
            return Descriptor.Resolve(this, Args);
        }

    }
}
