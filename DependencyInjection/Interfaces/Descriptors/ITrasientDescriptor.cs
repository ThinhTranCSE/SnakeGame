using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Interfaces.Descriptors
{
    public interface ITrasientDescriptor<TImplementation>
        where TImplementation : class
    {
    }
}
