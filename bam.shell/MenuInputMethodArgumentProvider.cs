using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Provides method arguments by parsing comma-separated values from menu input and converting them using a typed argument provider.
    /// </summary>
    public class MenuInputMethodArgumentProvider : IMenuInputMethodArgumentProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuInputMethodArgumentProvider"/> class.
        /// </summary>
        /// <param name="parameterProvider">The typed argument provider used to convert string inputs to typed arguments.</param>
        public MenuInputMethodArgumentProvider(ITypedArgumentProvider parameterProvider)
        {
            this.TypedArgumentProvider = parameterProvider;
        }

        /// <summary>
        /// Gets or sets the typed argument provider used for conversion.
        /// </summary>
        public ITypedArgumentProvider TypedArgumentProvider { get; set; }

        /// <summary>
        /// Gets the method arguments by parsing the menu input value as comma-separated strings and converting each to the target parameter type.
        /// </summary>
        /// <param name="menuItem">The menu item whose method requires arguments.</param>
        /// <param name="menuInput">The input containing comma-separated argument values.</param>
        /// <returns>An array of typed arguments matching the method's parameters.</returns>
        public virtual object?[] GetMethodArguments(IMenuItem menuItem, IMenuInput menuInput)
        {
            Args.ThrowIfNull(menuItem, nameof(menuItem));
            Args.ThrowIfNull(menuInput, nameof(menuInput));

            ParameterInfo[] parameterInfos = menuItem.MethodInfo.GetParameters();

            if(parameterInfos.Length  == 0 )
            {
                return Array.Empty<object>();
            }

            string[]  inputStrings = menuInput.Value.Split(',');
            if(inputStrings.Length != parameterInfos.Length)
            {
                throw new ArgumentException("argument count mismatch");
            }
            object?[] arguments = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                arguments[i] = TypedArgumentProvider.GetTypedArgument(parameterInfos[i], inputStrings[i]);
            }

            return arguments;
        }
    }
}
