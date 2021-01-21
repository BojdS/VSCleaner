using System.Collections.Generic;

namespace VSCleaner.Core.Contracts
{
    public interface IDirectoryCleaner
    {
        IEnumerable<string> CleanDirectory(string path);
    }
}