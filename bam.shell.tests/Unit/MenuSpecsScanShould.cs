using System.Reflection;
using Bam.DependencyInjection;
using Bam.Test;
using Bam.Services;

namespace Bam.Shell.Tests.Unit
{
    public class MenuSpecsScanShould : UnitTestMenuContainer
    {
        public MenuSpecsScanShould(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
        }

        [UnitTest]
        public void SeedItemAttributeTypesFromMenuAttribute()
        {
            When.A<MenuSpecs>("scan seeds item attribute types from the container's MenuAttribute.ItemAttributeType",
                new MenuSpecs(typeof(SeededMenuContainer)),
                (specs) =>
                {
                    MenuSpecs? scanned = ScanFor(typeof(SeededMenuContainer));
                    return new ScanOutcome(
                        scanned != null,
                        scanned?.ItemAttributeTypes.Contains(typeof(TestCommandAttribute)) ?? false,
                        scanned?.ItemAttributeTypes.Count ?? -1);
                })
            .TheTest
            .ShouldPass<ScanOutcome>((because, outcome) =>
            {
                because.ItsTrue("a spec was discovered for the container", outcome.SpecFound);
                because.ItsTrue("the declared item attribute type was seeded without any [MenuItem] methods", outcome.ContainsDeclaredItemType);
                because.ItsTrue("exactly one item attribute type was seeded", outcome.ItemTypeCount == 1);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void SeedItemAttributeTypesForMethodlessContainer()
        {
            When.A<MenuSpecs>("scan seeds item attribute types for a container that declares no methods of its own",
                new MenuSpecs(typeof(MethodlessMenuContainer)),
                (specs) =>
                {
                    MenuSpecs? scanned = ScanFor(typeof(MethodlessMenuContainer));
                    return new ScanOutcome(
                        scanned != null,
                        scanned?.ItemAttributeTypes.Contains(typeof(TestCommandAttribute)) ?? false,
                        scanned?.ItemAttributeTypes.Count ?? -1);
                })
            .TheTest
            .ShouldPass<ScanOutcome>((because, outcome) =>
            {
                because.ItsTrue("a spec was discovered for the methodless container", outcome.SpecFound);
                because.ItsTrue("the declared item attribute type was seeded", outcome.ContainsDeclaredItemType);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void StillDiscoverMenuItemDecoratedMethods()
        {
            When.A<MenuSpecs>("scan still discovers [MenuItem]-decorated methods on a base MenuAttribute container",
                new MenuSpecs(typeof(MenuItemOnlyContainer)),
                (specs) =>
                {
                    MenuSpecs? scanned = ScanFor(typeof(MenuItemOnlyContainer));
                    return new ScanOutcome(
                        scanned != null,
                        scanned?.ItemAttributeTypes.Contains(typeof(MenuItemAttribute)) ?? false,
                        scanned?.ItemAttributeTypes.Count ?? -1);
                })
            .TheTest
            .ShouldPass<ScanOutcome>((because, outcome) =>
            {
                because.ItsTrue("a spec was discovered for the container", outcome.SpecFound);
                because.ItsTrue("the MenuItem attribute type was discovered from the method scan", outcome.ContainsDeclaredItemType);
                because.ItsTrue("exactly one item attribute type was discovered", outcome.ItemTypeCount == 1);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void YieldEmptySpecForBaseMenuAttributeWithoutMenuItems()
        {
            When.A<MenuSpecs>("scan yields an empty spec for a base MenuAttribute container with no [MenuItem] methods",
                new MenuSpecs(typeof(UnseededMenuContainer)),
                (specs) =>
                {
                    MenuSpecs? scanned = ScanFor(typeof(UnseededMenuContainer));
                    return new ScanOutcome(
                        scanned != null,
                        false,
                        scanned?.ItemAttributeTypes.Count ?? -1);
                })
            .TheTest
            .ShouldPass<ScanOutcome>((because, outcome) =>
            {
                because.ItsTrue("a spec was discovered for the container", outcome.SpecFound);
                because.ItsTrue("no item attribute types were seeded (null ItemAttributeType is guarded)", outcome.ItemTypeCount == 0);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void UnionSeededAndMenuItemAttributeTypes()
        {
            When.A<MenuSpecs>("scan unions seeded item attribute types with [MenuItem]-decorated method types",
                new MenuSpecs(typeof(MixedMenuContainer)),
                (specs) =>
                {
                    MenuSpecs? scanned = ScanFor(typeof(MixedMenuContainer));
                    bool containsSeeded = scanned?.ItemAttributeTypes.Contains(typeof(TestCommandAttribute)) ?? false;
                    bool containsMenuItem = scanned?.ItemAttributeTypes.Contains(typeof(MenuItemAttribute)) ?? false;
                    return new UnionScanOutcome(
                        scanned != null,
                        containsSeeded,
                        containsMenuItem,
                        scanned?.ItemAttributeTypes.Count ?? -1);
                })
            .TheTest
            .ShouldPass<UnionScanOutcome>((because, outcome) =>
            {
                because.ItsTrue("a spec was discovered for the container", outcome.SpecFound);
                because.ItsTrue("the seeded item attribute type is present", outcome.ContainsSeededType);
                because.ItsTrue("the [MenuItem] attribute type is present", outcome.ContainsMenuItemType);
                because.ItsTrue("the two sources are unioned without duplicates", outcome.ItemTypeCount == 2);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        private static MenuSpecs? ScanFor(Type containerType)
        {
            return MenuSpecs.Scan(Assembly.GetExecutingAssembly())
                .FirstOrDefault(specs => specs.ContainerType == containerType);
        }

        private sealed record ScanOutcome(bool SpecFound, bool ContainsDeclaredItemType, int ItemTypeCount);

        private sealed record UnionScanOutcome(bool SpecFound, bool ContainsSeededType, bool ContainsMenuItemType, int ItemTypeCount);
    }

    /// <summary>
    /// Fixture item attribute standing in for a concrete item marker such as a console command attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TestCommandAttribute : Attribute
    {
    }

    /// <summary>
    /// Fixture menu attribute mirroring how concrete menus derive from <see cref="MenuAttribute{TAttr}"/>
    /// to declare their item attribute type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TestMenuAttribute : MenuAttribute<TestCommandAttribute>
    {
        public TestMenuAttribute(string name) : base(name)
        {
        }
    }

    [TestMenu("seeded")]
    public class SeededMenuContainer
    {
        [TestCommand]
        public void DoSomething()
        {
        }
    }

    [TestMenu("methodless")]
    public class MethodlessMenuContainer
    {
    }

    [Menu("menu-item-only")]
    public class MenuItemOnlyContainer
    {
        [MenuItem("do something")]
        public void DoSomething()
        {
        }
    }

    [Menu("unseeded")]
    public class UnseededMenuContainer
    {
        public void NotACommand()
        {
        }
    }

    [TestMenu("mixed")]
    public class MixedMenuContainer
    {
        [TestCommand]
        public void SeededCommand()
        {
        }

        [MenuItem("legacy item")]
        public void LegacyItem()
        {
        }
    }
}
