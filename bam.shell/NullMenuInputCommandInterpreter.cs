namespace Bam.Shell
{
    /// <summary>
    /// A no-op implementation of <see cref="IMenuInputCommandInterpreter"/> that always returns false, indicating no input was interpreted.
    /// </summary>
    public class NullMenuInputCommandInterpreter : IMenuInputCommandInterpreter
    {
        /// <inheritdoc/>
        public bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IInputCommandResults result)
        {
            result = new InputCommandResults();
            return false;
        }
    }
}
