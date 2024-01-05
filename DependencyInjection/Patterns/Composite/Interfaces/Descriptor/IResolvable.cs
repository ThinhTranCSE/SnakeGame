using DependencyInjection.Interfaces.DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Patterns.Composite.Interfaces.Descriptor
{
    public interface IResolvable
    {
        Type ImplementationType { get; }
        //object Resolve(IDIContainer Container, params Type[] Args);
        object Resolve(IDIContainer Container, params object[] Args);
    }
}
