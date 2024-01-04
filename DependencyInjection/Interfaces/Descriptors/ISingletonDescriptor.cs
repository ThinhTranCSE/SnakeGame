using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Interfaces.Descriptors
{
    public interface ISingletonDescriptor<TImplementation> 
        where TImplementation : class
    {
       TImplementation Implementation { get; }
    }
}
