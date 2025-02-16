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
            testOne.Equals(testTwo).ShouldBeTrue("MenuSpecs should have been equal");
        }
    }
}
