namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for providing method arguments derived from menu input.
    /// </summary>
    public interface IMenuInputMethodArgumentProvider
    {
        /// <summary>
        /// Gets the method arguments for the specified menu item based on the menu input.
        /// </summary>
        /// <param name="menuItem">The menu item whose method requires arguments.</param>
        /// <param name="menuInput">The input to derive argument values from.</param>
        /// <returns>An array of arguments to pass to the method.</returns>
        object?[] GetMethodArguments(IMenuItem menuItem, IMenuInput menuInput);
    }
}
