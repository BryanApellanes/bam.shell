using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class InputCommandResults : IInputCommandResults
    {
        List<IInputCommandResult> _results;
        public InputCommandResults()
        {
            _results = new List<IInputCommandResult>();
        }

        public bool Sucess 
        {
            get
            {
                return Exception == null;
            }
        }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public IEnumerable<IInputCommandResult> Results
        {
            get
            {
                return _results;
            }
        }

        public void AddResult(IInputCommandResult result)
        {
            _results.Add(result);
        }
    }
}
