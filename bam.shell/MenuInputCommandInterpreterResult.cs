namespace Bam.Shell
{
    public class MenuInputCommandInterpreterResult : IMenuInputCommandInterpreterResult
    {
        List<IInputCommandResult> menuItemRunResults = new List<IInputCommandResult>();
        public MenuInputCommandInterpreterResult() 
        {
        }

        public IEnumerable<IInputCommandResult?> MenuItemRunResults
        {
            get
            {
                return menuItemRunResults;
            }
        }

        public MenuInputCommandInterpreterResult AddResult(object? result)
        {
            return AddResult(new MenuItemRunResult
            {
                Success = true,
                Result = result,
            });
        }

        public MenuInputCommandInterpreterResult AddResult(IInputCommandResult? result)
        {
            menuItemRunResults.Add(result);
            return this;
        }
    }
}
