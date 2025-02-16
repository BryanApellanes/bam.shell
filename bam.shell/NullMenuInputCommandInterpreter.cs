namespace Bam.Shell
{
    public class NullMenuInputCommandInterpreter : IMenuInputCommandInterpreter
    {
        public bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IInputCommandResults result)
        {
            result = new InputCommandResults();
            return false;
        }
    }
}
