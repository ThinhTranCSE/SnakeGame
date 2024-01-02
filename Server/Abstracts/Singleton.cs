using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Abstracts
{
    public abstract class Singleton<T>
    {
        protected static T _Instance;
        public static T Instance => GetInstance();
        public static T GetInstance()
        {
            if(_Instance == null)
            {
                _Instance = (T)Activator.CreateInstance(typeof(T), true);
            }
            return _Instance;
        }
    }
}
