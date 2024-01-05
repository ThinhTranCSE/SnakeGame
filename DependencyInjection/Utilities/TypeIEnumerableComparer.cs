using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Utilities
{
    public class TypeIEnumerableComparer : IEqualityComparer<IEnumerable<Type>>
    {
        public bool Equals(IEnumerable<Type> x, IEnumerable<Type> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(IEnumerable<Type> obj)
        {
            int HashCode = 0;
            foreach(Type T in obj)
            {
                HashCode ^= T.GetHashCode();
            }
            return HashCode;
        }
    }
}
