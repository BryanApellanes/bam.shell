namespace Bam.Shell
{
    public interface IMenuInputCommandInterpreterResult
    {
        IEnumerable<IInputCommandResult?> MenuItemRunResults { get; }

        MenuInputCommandInterpreterResult AddResult(object? result);
        MenuInputCommandInterpreterResult AddResult(IInputCommandResult? result);
    }
}
