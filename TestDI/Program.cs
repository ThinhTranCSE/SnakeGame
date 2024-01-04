using DependencyInjection.Implementations.ServiceCollections;
using DependencyInjection.Interfaces.DIContainer;

namespace TestDI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //typeof(Test).GetConstructors().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            PrintTest();
        }
        internal static void PrintTest()
        {
            ServiceCollection Collection = new ServiceCollection();
            Collection.StartRegister().RegisterType<Test>().Transient().As<ITest>().EndRegister();
            Collection.StartRegister().RegisterType<Dependency>().Transient().As<IDependency>().EndRegister();
            IDIContainer Container = Collection.BuildContainer();

            ITest Test = Container.Resolve<ITest>();
            ITest Test2 = Container.Resolve<ITest>();

            Console.WriteLine(Test.Equals(Test2));
        }
    }

    internal interface ITest
    {
        
    }

    internal class Test : ITest
    {
        public IDependency Dependency;
        public Test(IDependency Dependency)
        {
            this.Dependency = Dependency;
        }

        public Test(int Dependency)
        {
            
        }

        public Test(string Dependency)
        {

        }

        public Test(bool Dependency)
        {
            
        }
    }

    internal interface IDependency
    {
        
    }

    internal class Dependency : IDependency
    {
        public Dependency()
        {

        }
    }


}