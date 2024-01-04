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
    public class TransientDescriptor<TInterface, TImplementation> : ServiceDescriptor<TInterface, TImplementation>, ITrasientDescriptor<TImplementation>
        where TInterface : class
        where TImplementation : class, TInterface
    {
        public TransientDescriptor(IDictionary<IEnumerable<Type>, ConstructorInfo> Constructors) : base(ServiceLifetime.TRANSIENT, Constructors)
        {

        }


        public override object Resolve(IDIContainer Container, params Type[] Args)
        {
            return base.Resolve(Container, Args);
        }
    }
}
