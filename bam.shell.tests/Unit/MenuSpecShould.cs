using Bam.Net;
using Bam.Net.CoreServices;
using Bam.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override ServiceRegistry Configure(ServiceRegistry serviceRegistry)
        {
            return base.Configure(serviceRegistry);
        }
    }
}
