using DependencyInjection.Enums;
using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.ServiceCollections;
using DependencyInjection.Patterns.Builder.Interfaces.Descriptor;
using DependencyInjection.Patterns.Composite.Interfaces.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Implementations.Descriptors
{
    public class DescriptorBuilder : IDescriptorBuilder
    {
        private IServiceCollection Collection;
        public Type ServiceType { get; private set; }

        public Type ImplementationType { get; private set; }

        public ServiceLifetime Lifetime { get; private set; } = ServiceLifetime.TRANSIENT;

        public DescriptorBuilder(IServiceCollection Collection)
        {
            this.Collection = Collection;
        }

        public IDescriptorBuilder RegisterType(Type ImplementationType)
        {
            if (ServiceType != null && !ServiceType.IsAssignableFrom(ImplementationType))
            {
                throw new Exception($"{ImplementationType} is not assignable from {ServiceType}");
            }
            this.ImplementationType = ImplementationType;
            return this;
        }

        public IDescriptorBuilder RegisterType<TImplementation>()
        {
            return RegisterType(typeof(TImplementation));
        }

        public IDescriptorBuilder As<TInterface>()
        {
            return As(typeof(TInterface));
        }

        public IDescriptorBuilder As(Type InterfaceType)
        {
            if(ImplementationType != null && !InterfaceType.IsAssignableFrom(ImplementationType))
            {
                throw new Exception($"{ImplementationType} are not implemented from {InterfaceType}");
            }
            ServiceType = InterfaceType;
            return this;
        }

        public IDescriptorBuilder Singleton()
        {
            return WithLifetime(ServiceLifetime.SINGLETON);
        }

        public IDescriptorBuilder Transient()
        {
            return WithLifetime(ServiceLifetime.TRANSIENT);
        }

        public IDescriptorBuilder WithLifetime(ServiceLifetime Lifetime)
        {
            this.Lifetime = Lifetime;
            return this;
        }

        public IServiceDescriptor EndRegister()
        {
            if(ServiceType == null)
            {
                throw new Exception("Must specify service type before registering");
            }
            if(ImplementationType == null)
            {
                throw new Exception("Must specify implementation type before registering");
            }
            if(Lifetime == null)
            {
                throw new Exception("Must specify lifetime before registering");
            }
            IDictionary<IEnumerable<Type>, ConstructorInfo> ConstructorDictionary = new Dictionary<IEnumerable<Type>, ConstructorInfo>();

            var Constructors = ImplementationType.GetConstructors().ToArray();
            foreach(var Constructor in Constructors)
            {
                var Parameters = Constructor.GetParameters();
                if(Parameters.Length == 0)
                {
                    ConstructorDictionary.Add(Array.Empty<Type>(), Constructor);
                }
                else
                {
                    ConstructorDictionary.Add(Parameters.Select(p => p.ParameterType), Constructor);
                }
            }

            //if(DependencyLists.Count != 0)
            //{
            //    foreach (List<Type> Dependencies in DependencyLists)
            //    {
            //        var Constructor = ImplementationType.GetConstructor(Dependencies.ToArray());
            //        if (Constructor == null)
            //        {
            //            throw new Exception($"No constructor found for {ImplementationType} with parameters {string.Join(", ", Dependencies)}");
            //        }
            //        ConstructorDictionary.Add(Dependencies, Constructor);
            //    }
            //}
            //else
            //{
            //    var FirstConstructor = ImplementationType.GetConstructors().FirstOrDefault();
            //    if(FirstConstructor == null)
            //    {
            //        throw new Exception($"No constructor found for {ImplementationType}");
            //    }
            //    ConstructorDictionary.Add(FirstConstructor.GetParameters().Select(p => p.ParameterType), FirstConstructor);
            //}


            IServiceDescriptor? Descriptor;
            switch (Lifetime)
            {
                case ServiceLifetime.SINGLETON: 
                    ConstructorInfo? SingletonDescriptorConstructor = typeof(SingletonDescriptor<,>)
                        .MakeGenericType(ServiceType, ImplementationType)
                        .GetConstructor(new Type[] { typeof(IDictionary<IEnumerable<Type>, ConstructorInfo>) });
                    Descriptor = SingletonDescriptorConstructor?.Invoke(new object?[] { ConstructorDictionary }) as IServiceDescriptor;
                    break;

                case ServiceLifetime.TRANSIENT:
                    ConstructorInfo? TransientDescriptorConstructor = typeof(TransientDescriptor<,>)
                        .MakeGenericType(ServiceType, ImplementationType)
                        .GetConstructor(new Type[] { typeof(IDictionary<IEnumerable<Type>, ConstructorInfo>) });
                    Descriptor = TransientDescriptorConstructor?.Invoke(new object?[] { ConstructorDictionary }) as IServiceDescriptor;
                    break;
                default: throw new Exception("Invalid lifetime");
            }
            if(Descriptor == null)
            {
                throw new Exception("Could not create descriptor");
            }

            Collection.Descriptors.Add(ServiceType, Descriptor);
            return (IServiceDescriptor)Descriptor;
        }
    }
}
