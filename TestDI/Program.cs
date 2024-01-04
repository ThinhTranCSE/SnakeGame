using DependencyInjection.Implementations.ServiceCollections;
using DependencyInjection.Interfaces.DIContainer;

namespace TestDI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<IEnumerable<Type>, int> dict = new Dictionary<IEnumerable<Type>, int>();
            //dict.Add(new Type[] { typeof(int), typeof(string) }, 1);
            //Console.WriteLine(dict.ContainsKey(new Type[] { typeof(int), typeof(string) }));

            PrintTest();
        }
        internal static void PrintTest()
        {
            ServiceCollection Collection = new ServiceCollection();
            Collection.StartRegister().RegisterType<Test>().Transient().As<ITest>().EndRegister();
            //Collection.StartRegister().RegisterType<Dependency>().Transient().As<IDependency>().EndRegister();
            IDIContainer Container = Collection.BuildContainer();

            ITest Test = Container.Resolve<ITest>(1);
            ITest Test2 = Container.Resolve<ITest>(true);

            Console.WriteLine(Test.Equals(Test2));
        }
    }

    internal interface ITest
    {
        
    }

    internal class Test : ITest
    {
        public IDependency Dependency;
        public IDependency2 Dependency2;
        public Test(IDependency Dependency, IDependency2 Dependency2)
        {
            this.Dependency = Dependency;
            this.Dependency2 = Dependency2;
        }

        public Test(int Dependency)
        {
            Console.WriteLine("int");
        }

        public Test(string Dependency)
        {
            Console.WriteLine("string");
        }

        public Test(bool Dependency)
        {
            Console.WriteLine("bool");
        }
    }

    internal interface IDependency
    {
        
    }

    internal interface IDependency2
    {

    }
    internal class Dependency : IDependency
    {
        public Dependency()
        {

        }
    }

    internal class Dependency2 : IDependency2
    {
        public Dependency2()
        {

        }
    }

}