namespace Bam.Shell
{
    public interface IInputCommandResults
    {
        Exception Exception { get; set; }
        string Message { get; set; }
        IEnumerable<IInputCommandResult> Results { get; }
        bool Sucess { get; }

        void AddResult(IInputCommandResult result);
    }
}