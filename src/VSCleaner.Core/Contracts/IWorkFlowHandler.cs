using System.Collections.Generic;

namespace VSCleaner.Core.Contracts
{
    public interface IWorkFlowHandler
    {
        public IEnumerable<string> Handle(string[] arguments);
    }
}