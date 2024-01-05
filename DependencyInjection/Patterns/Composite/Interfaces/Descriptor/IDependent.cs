using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Patterns.Composite.Interfaces.Descriptor
{
    public interface IDependent
    {
        IDictionary<IEnumerable<Type>, IEnumerable<IResolvable?>> DependencyDictionary { get; }
        //void AddDependencies(IEnumerable<Type> Dependencies);
        //void RemoveDependencies(IEnumerable<Type> Dependencies);
    }
}
