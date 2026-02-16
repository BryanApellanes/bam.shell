namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for rendering input command results to the output.
    /// </summary>
    public interface IInputCommandResultRenderer
    {
        /// <summary>
        /// Renders the specified input command result.
        /// </summary>
        /// <param name="inputCommandResult">The input command result to render.</param>
        void RenderInputCommandResult(IInputCommandResult inputCommandResult);
    }
}
