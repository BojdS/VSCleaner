using System;
using System.Collections.Generic;

namespace VSCleaner.Core.Contracts
{
    public interface IArgumentsResolver
    {
        List<Func<IEnumerable<string>>> ProcessArguments(IDictionary<string, string> argumentsDictionary);
    }
}