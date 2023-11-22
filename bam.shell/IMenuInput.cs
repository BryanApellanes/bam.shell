using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuInput
    {
        /// <summary>
        /// Gets the input.
        /// </summary>
        StringBuilder Input { get; }

        /// <summary>
        /// Gets a value indicating whether the disgnated exit key was pressed, the default is the escape key.
        /// </summary>
        bool Exit { get; }

        /// <summary>
        /// Gets the exit code.
        /// </summary>
        int ExitCode { get; }

        /// <summary>
        /// Gets a value indicating whether the enter key was pressed.
        /// </summary>
        bool Enter { get; }

        /// <summary>
        /// Gets a value indicating if a menu item navigation key was pressed, the default keys are down arrow and up arrow.
        /// </summary>
        bool IsMenuItemNavigation { get; }

        /// <summary>
        /// Gets a value indicating if a menu navigation key was pressed, the default keys are left arrow and right arrow.
        /// </summary>
        bool IsMenuNavigation { get; }

        /// <summary>
        /// Gets a value indicating if the input should be interpreted as a selector.
        /// </summary>
        bool IsSelector { get; }

        /// <summary>
        /// Gets the input as a string.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the selector.
        /// </summary>
        string Selector { get; }

        /// <summary>
        /// Gets the item number.
        /// </summary>
        int ItemNumber { get; }

        /// <summary>
        /// Gets a value indicating if the next item key was pressed, the default is the down arrow.
        /// </summary>
        bool NextItem { get; }

        /// <summary>
        /// Gets a value indicating if the previous item key was pressed, the default is the up arrow.
        /// </summary>
        bool PreviousItem { get; }

        /// <summary>
        /// Gets a value indicating if the next menu key was pressed, the default is the right arrow.
        /// </summary>
        bool NextMenu { get; }

        /// <summary>
        /// Gets a value indicating if the previous menu key was pressed, the default is the left arrow.
        /// </summary>
        bool PreviousMenu { get; }
    }
}
