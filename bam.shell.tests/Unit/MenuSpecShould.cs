using Bam.DependencyInjection;
using Bam.Test;
using Bam.Services;

namespace Bam.Shell.Tests.Unit
{
    public class MenuSpecShould : UnitTestMenuContainer
    {
        public MenuSpecShould(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
        }

        [UnitTest]
        public void BeEqualIfSameTypes()
        {
            MenuSpec testOne = new MenuSpec(typeof(MenuSpecShould), typeof(UnitTest));
            MenuSpec testTwo = new MenuSpec(typeof(MenuSpecShould), typeof(UnitTest));

            When.A<MenuSpec>("is equal to another MenuSpec with the same types",
                testOne,
                (spec) => spec)
            .TheTest
            .ShouldPass(because =>
            {
                because.ItsTrue("MenuSpecs with same types are equal", testOne.Equals(testTwo));
            })
            .SoBeHappy()
            .UnlessItFailed();
        }
    }
}
