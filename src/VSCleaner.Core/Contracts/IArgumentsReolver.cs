using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSCleaner.Core.Contracts
{
    public interface IArgumentsResolver
    {
        IEnumerable<Func<Task>> ResolveParameters(IDictionary<string, string> parametersDictionary);
    }
}