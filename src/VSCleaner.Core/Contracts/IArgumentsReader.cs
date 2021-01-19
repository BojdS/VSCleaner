using System.Collections.Generic;

namespace VSCleaner.Core.Contracts
{
    public interface IArgumentsReader
    {
        IDictionary<string, string> ReadArgumentsToDictionary(string[] arguments);
    }
}