using DependencyInjection.Enums;
using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.DIContainer;
using DependencyInjection.Patterns.Composite.Interfaces.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DependencyInjection.Interfaces.Descriptors.IServiceDescriptor;

namespace DependencyInjection.Abstracts.Descriptors
{
    public abstract class ServiceDescriptor<TInterface, TImplementation> : IServiceDescriptor
        where TInterface: class
        where TImplementation : class, TInterface
    {
        public Type ServiceType { get; private set; }
        public Type ImplementationType { get; private set; }

        public ServiceLifetime Lifetime { get; private set; }

        public IDictionary<IEnumerable<Type>, ConstructorInfo> ConstructorDictionary { get; private set; }

        public IDictionary<IEnumerable<Type>, IEnumerable<IResolvable>> DependencyDictionary { get; set;}

        public ServiceDescriptor(ServiceLifetime Lifetime, IDictionary<IEnumerable<Type>, ConstructorInfo> Contructors)
        {
            ServiceType = typeof(TInterface);
            ImplementationType = typeof(TImplementation);
            this.Lifetime = Lifetime;
            ConstructorDictionary = Contructors;
            DependencyDictionary = new Dictionary<IEnumerable<Type>, IEnumerable<IResolvable>>();
        }

        private object ResolveWithTypes(IDIContainer Container, params Type[] ArgTypes)
        {
            ConstructorInfo Constructor;
            if (ArgTypes.Length == 0)
            {
                if (ConstructorDictionary.ContainsKey(ArgTypes))
                {
                    Constructor = ConstructorDictionary[ArgTypes];
                    return Constructor.Invoke(null);
                }
                else
                {
                    Constructor = ConstructorDictionary.First().Value;
                }
            }
            else
            {
                if (!ConstructorDictionary.ContainsKey(ArgTypes))
                {
                    throw new Exception("No constructor found for given arguments");
                }
                Constructor = ConstructorDictionary[ArgTypes];
            }
            var ParamInfos = Constructor.GetParameters();
            ParamInfos.ToList().ForEach(ParamInfo =>
            {
                if (!Container.Descriptors.ContainsKey(ParamInfo.ParameterType))
                {
                    throw new Exception($"No service for type {ParamInfo.ParameterType.Name} has been registered. It was used in {ImplementationType}'s constructor");
                }
            });
            object[] Args = Constructor.GetParameters().Select(ParamInfo => Container.Resolve(ParamInfo.ParameterType)).ToArray();
            return Constructor.Invoke(Args);
        }

        public virtual object Resolve(IDIContainer Container, params object[] Args)
        {
            if(Args.All(Arg => Arg.GetType() == typeof(Type)))
            {
                return ResolveWithTypes(Container, Args.Select(Arg => (Type)Arg).ToArray());
            }

            Type[] ArgsTypes = Args.Select(Arg => Arg.GetType()).ToArray();
            if(!ConstructorDictionary.ContainsKey(ArgsTypes))
            {
                throw new Exception("No constructor found for given arguments");
            }
            ConstructorInfo Constructor = ConstructorDictionary[ArgsTypes];
            return Constructor.Invoke(Args);
        }
    }
}
