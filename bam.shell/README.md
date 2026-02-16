# bam.shell

Abstract menu system framework providing interfaces and implementations for interactive console menu navigation, rendering, and item execution.

## Overview

bam.shell defines the core menu system abstractions used throughout the Bam Framework. It provides a complete menu lifecycle: discovering menu items from attributed methods, rendering menus to the console, reading user input, navigating between menus, selecting and running menu items, and interpreting input commands. The framework is designed around interfaces so that rendering, input, and execution behaviors can be customized via dependency injection.

Menus are defined by adorning classes with `[MenuAttribute]` (or its generic variant `[Menu<TItemAttribute>]`) and methods with `[MenuItemAttribute]` or custom subclasses. The `MenuSpecs` class scans assemblies to discover these attributed types and builds menu specifications. The `MenuManager` orchestrates the runtime lifecycle: it loads menus from specs, manages the current menu selection, handles keyboard navigation (next/previous menu, next/previous item), delegates item execution through `IMenuItemRunner`, and runs the input-output loop via `MenuInputOutputLoop`.

The architecture follows a provider pattern: `MenuItemProvider` discovers menu items from types, `MenuProvider` creates `Menu` instances, `MenuItemSelector` handles selection logic, and `MenuRenderer` controls display. All components are wired together through `MenuOptions` (or the console-specific `ConsoleMenuOptions` from bam.console). The system supports multiple menus per container type (one per item attribute type) and provides input commands (typed at the prompt) alongside numbered menu item selection.

## Key Classes

| Class | Description |
|---|---|
| `IMenu` / `Menu` | Represents a single menu with items, selection state, header/footer text, and run capabilities. |
| `IMenuManager` / `MenuManager` | Orchestrates multiple menus: loading, navigation, selection, rendering, and running the input-output loop. |
| `MenuInputOutputLoop` | Internal loop that renders menus, reads input, interprets commands, runs selected items, and re-renders. |
| `MenuAttribute` / `MenuAttribute<T>` | Class-level attribute defining a menu. Generic variant specifies the item attribute type. |
| `MenuItemAttribute` | Method-level attribute marking a method as a menu item with optional display name, selector, and description. |
| `MenuItem` / `MenuItem<T>` | Runtime representation of a menu item wrapping a `MethodInfo` and its attribute metadata. |
| `MenuSpec` | Pair of (ContainerType, ItemAttributeType) identifying a specific menu configuration. |
| `MenuSpecs` | Scans assemblies for `[Menu]`-attributed types and their item attribute types. Maintains a global `LoadList`. |
| `MenuProvider` / `MenuProvider<T>` | Creates `Menu` instances from types, caches by selector. |
| `MenuItemProvider` | Discovers `[MenuItemAttribute]`-adorned methods on a container type and yields `MenuItem` instances. |
| `MenuItemSelector` | Handles menu item selection by index, selector string, or navigation (next/previous). |
| `MenuOptions` | Data class holding all menu system component references (renderer, provider, input reader, etc.). |
| `MenuInput` | Represents user input including key presses, text, navigation flags, and enter/exit states. |
| `InputCommandResults` | Collects results from input command execution. |
| `NullMenuInputCommandInterpreter` | No-op interpreter that always returns false, used when no input commands are configured. |
| `ServiceRegistryTypedArgumentProvider` | Stub `ITypedArgumentProvider` backed by a `ServiceRegistry`. |

## Dependencies

**Project References:**
- `bam.base` -- Core framework primitives, DI, string extensions, reflection helpers

**Target Framework:** net10.0
**Output Type:** Library

## Usage Examples

### Define a menu container
```csharp
using Bam.Shell;

[Menu<MenuItemAttribute>("My Operations")]
public class MyOperations
{
    [MenuItem("Say Hello", Description = "Prints a greeting")]
    public void SayHello()
    {
        Console.WriteLine("Hello!");
    }

    [MenuItem("Say Goodbye")]
    public void SayGoodbye()
    {
        Console.WriteLine("Goodbye!");
    }
}
```

### Scan assemblies for menus
```csharp
IEnumerable<MenuSpecs> specs = MenuSpecs.Scan(Assembly.GetEntryAssembly());
```

### Create a MenuManager and start the loop
```csharp
MenuManager manager = MenuManager.FromOptions(menuOptions);
manager.LoadMenus(specs);
manager.StartInputOutputLoop();
```

### Programmatic menu navigation
```csharp
IMenu? menu = manager.SelectMenu("mySelector");
IMenuItem? item = menu?.SelectItem("sh"); // select by acronym selector
IMenuItemRunResult? result = manager.RunMenuItem(item);
```

## Known Gaps / Not Yet Implemented

- `ServiceRegistryTypedArgumentProvider.GetTypedArgument` throws `NotImplementedException`. This provider is defined but not yet functional; the `StringArgumentProvider` (from bam.console) is used by default instead.
