using System.Text;

namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuInput"/> representing user input for the menu system.
    /// </summary>
    public class MenuInput : IMenuInput
    {
        static MenuInput _instance;
        static object _instanceLock = new object();

        /// <summary>
        /// Gets a singleton empty menu input instance.
        /// </summary>
        public static IMenuInput Empty
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new MenuInput());
            }
        }

        /// <summary>
        /// Creates a <see cref="MenuInput"/> from the specified command-line arguments, joined as a space-separated string.
        /// </summary>
        /// <param name="arguments">The command-line arguments to use as input.</param>
        /// <returns>A new <see cref="MenuInput"/> instance.</returns>
        public static MenuInput FromArguments(string[] arguments)
        {
            return new MenuInput()
            {
                Input = new StringBuilder(string.Join(" ", arguments))
            };
        }

        /// <inheritdoc/>
        public StringBuilder Input
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool Exit
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public int ExitCode
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool Enter
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool IsMenuItemNavigation
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool IsMenuNavigation
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool IsSelector
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public string Value
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public string Selector
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public int ItemNumber
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool NextItem
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool PreviousItem
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool NextMenu
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public bool PreviousMenu
        {
            get;
            set;
        }
    }
}
