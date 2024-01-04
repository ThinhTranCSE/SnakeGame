using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class DIContainer
    {
        public static Dictionary<Type, object> RegisteredModules { get; set; } = new Dictionary<Type, object>();

        public static void SetModule<TInterface, TModule>()
        {
            SetModule(typeof(TInterface), typeof(TModule));
        }

        private static void SetModule(Type Interface, Type Module)
        {
            if(!Interface.IsAssignableFrom(Module))
            {
                throw new Exception("Module must implement Interface");
            }
            var FirstConstructor = Module.GetConstructors().FirstOrDefault();
            object ModuleInstance = null;
            if (!FirstConstructor.GetParameters().Any())
            {
                ModuleInstance = FirstConstructor.Invoke(null);
            }
            else
            {
                var Params = FirstConstructor.GetParameters();
                List<object> ParamsInstances = new List<object>();
                foreach(var Param in Params)
                {
                    ParamsInstances.Add(GetModule(Param.ParameterType));
                }
                ModuleInstance = FirstConstructor.Invoke(ParamsInstances.ToArray());
            }
            if(RegisteredModules.ContainsKey(Interface))
            {
                RegisteredModules[Interface] = ModuleInstance;
            }
            else
            {
                RegisteredModules.Add(Interface, ModuleInstance);
            }
        }

        public static object GetModule<TInterface>()
        {
            return GetModule(typeof(TInterface));
        }

        private static object GetModule(Type Interface)
        {
            if(RegisteredModules.ContainsKey(Interface))
            {
                return RegisteredModules[Interface];
            }
            else
            {
                throw new Exception("Module not found");
            }
        }
    }
}
