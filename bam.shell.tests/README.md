# bam.shell.tests

Unit tests for the bam.shell project, validating menu specification equality and core shell behaviors.

## Overview

bam.shell.tests is an executable test project that uses the Bam Framework's menu-driven test runner. The entry point calls `BamConsoleContext.StaticMain(args)`, which bootstraps the interactive menu system and allows running tests via the `--ut` switch.

The current test suite focuses on `MenuSpec` equality semantics, verifying that two `MenuSpec` instances with the same container type and item attribute type are considered equal. This is a foundational behavior used by `MenuManager` and `MenuProvider` to detect duplicate menus and manage menu registries.

The test project also references `bam.data.dynamic`, `bam.data.repositories`, `bam.data.schema`, and `bam.data` in addition to `bam.shell` and `bam.test`, suggesting it may be intended to host more comprehensive integration tests in the future.

## Key Classes

| Class | Description |
|---|---|
| `MenuSpecShould` | Unit test class validating `MenuSpec.Equals()` behavior for type-based equality comparison. |

## Dependencies

**Project References:**
- `bam.base` -- Core framework primitives
- `bam.data.dynamic` -- Dynamic data access
- `bam.data.repositories` -- Repository abstractions
- `bam.data.schema` -- Schema management
- `bam.data` -- Data framework
- `bam.test` -- Test framework (`UnitTestMenuContainer`, `When`, `Because`, etc.)
- `bam.shell` -- The project under test

**Target Framework:** net10.0
**Output Type:** Exe

## Usage Examples

### Run all unit tests
```bash
dotnet run --project bam.shell.tests.csproj -- --ut
```

### Test structure example
```csharp
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
```

## Known Gaps / Not Yet Implemented

- The test suite is minimal (one test class, one test method). Additional tests for `MenuManager`, `MenuProvider`, `MenuItemProvider`, `MenuInputOutputLoop`, and navigation logic are not yet present.
- The `Integration\` folder is referenced in the .csproj but contains no test files.
