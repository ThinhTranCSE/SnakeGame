using DependencyInjection.Abstracts.Descriptors;
using DependencyInjection.Enums;
using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Implementations.Descriptors
{
    public class SingletonDescriptor<TInterface, TImplementation> : ServiceDescriptor<TInterface, TImplementation>, ISingletonDescriptor<TImplementation>
        where TInterface : class
        where TImplementation : class, TInterface
    {
        public TImplementation Implementation { get; private set; }
        public SingletonDescriptor(IDictionary<IEnumerable<Type>, ConstructorInfo> Constructors) : base(ServiceLifetime.SINGLETON, Constructors)
        {

        }

        public override object Resolve(IDIContainer Container, params object[] ArgTypes)
        {
            if(Implementation != null)
            {
                return Implementation;
            }
            Implementation = (TImplementation)base.Resolve(Container, ArgTypes);
            
            return Implementation;
        }
    }
}
