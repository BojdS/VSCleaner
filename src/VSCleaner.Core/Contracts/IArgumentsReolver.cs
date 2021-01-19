using System;
using System.Collections.Generic;

namespace VSCleaner.Core.Contracts
{
    public interface IArgumentsResolver
    {
        IEnumerable<Action> ResolveParameters(IDictionary<string, string> parametersDictionary);
    }
}