using DependencyInjection.Implementations.Descriptors;
using DependencyInjection.Implementations.DIContainers;
using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.DIContainer;
using DependencyInjection.Interfaces.ServiceCollections;
using DependencyInjection.Patterns.Builder.Interfaces.Descriptor;
using DependencyInjection.Patterns.Composite.Interfaces.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Implementations.ServiceCollections
{
    public class ServiceCollection : IServiceCollection
    {
        public Dictionary<Type, IServiceDescriptor> Descriptors { get; private set; }

        public ServiceCollection()
        {
            Descriptors = new Dictionary<Type, IServiceDescriptor>();
        }

        public IDIContainer BuildContainer()
        {
            foreach(IServiceDescriptor Descriptor in Descriptors.Values) 
            {
                foreach(IEnumerable<Type> ConstructorParams in Descriptor.ConstructorDictionary.Keys)
                {
                    List<IResolvable?> ResolvableDependencies = new List<IResolvable?>();
                    foreach(Type Param in ConstructorParams)
                    {
                        if(!Descriptors.ContainsKey(Param))
                        {
                            ResolvableDependencies.Add(null);
                            //throw new Exception($"Type {Param.Name} in {Descriptor.ImplementationType}'s Constructor is not registered");
                            continue;
                        }
                        IResolvable ParamDescriptor = Descriptors[Param];
                        ResolvableDependencies.Add(ParamDescriptor);
                    }
                    Descriptor.DependencyDictionary.Add(ConstructorParams, ResolvableDependencies);
                }
            }
            IDIContainer Container = new DIContainer(this);
            return Container;
        }

        public IDescriptorBuilder StartRegister()
        {
            return new DescriptorBuilder(this);
        }
    }
}
