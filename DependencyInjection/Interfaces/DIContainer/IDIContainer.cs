using DependencyInjection.Interfaces.Descriptors;
using DependencyInjection.Interfaces.ServiceCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Interfaces.DIContainer
{
    public interface IDIContainer
    {
        Dictionary<Type, IServiceDescriptor> Descriptors { get; }
        //TInterface Resolve<TInterface>(params Type[] ArgTypes)
        //    where TInterface : class;
        TInterface Resolve<TInterface>(params object[] Args)
            where TInterface : class;

        //object Resolve(Type InterfaceType, params Type[] ArgTypes);
        object Resolve(Type InterfaceType, params object[] Args);
    }
}
