using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for executing a menu item's associated method.
    /// </summary>
    public interface IMenuItemRunner
    {
        /// <summary>
        /// Gets the dependency provider used to resolve method arguments.
        /// </summary>
        IDependencyProvider DependencyProvider { get; }

        /// <summary>
        /// Gets or sets the provider that resolves method arguments from menu input.
        /// </summary>
        IMenuInputMethodArgumentProvider MethodArgumentProvider { get; set; }

        /// <summary>
        /// Runs the specified menu item's method with the given input.
        /// </summary>
        /// <param name="menuItem">The menu item to run.</param>
        /// <param name="menuInput">The input to pass to the method.</param>
        /// <returns>The result of running the menu item.</returns>
        IMenuItemRunResult RunMenuItem(IMenuItem menuItem, IMenuInput menuInput);
    }
}
