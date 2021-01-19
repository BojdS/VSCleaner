using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSCleaner.Core.Contracts;
using static VSCleaner.Core.Constants.VsCleanerConstants;
namespace VSCleaner.Core.Implementation
{
    
    public class ArgumentsResolver : IArgumentsResolver
    {
        public IEnumerable<Func<Task>> ResolveParameters(IDictionary<string, string> parametersDictionary)
        {
            foreach (var (k v) in parametersDictionary)
            {
                switch (k)
                {
                    case ArgumentsConstants.Path:
                    {
                        yield return () => Task.CompletedTask; 
                        break;
                    }
                    default:
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }
    }
}