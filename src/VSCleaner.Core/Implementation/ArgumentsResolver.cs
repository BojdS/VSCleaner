using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSCleaner.Core.Contracts;
using static VSCleaner.Core.Constants.VsCleanerConstants;
namespace VSCleaner.Core.Implementation
{
    
    public class ArgumentsResolver : IArgumentsResolver
    {
        public IEnumerable<Action> ResolveParameters(IDictionary<string, string> parametersDictionary)
        {
            foreach (var (k, v) in parametersDictionary)
            {
                switch (k)
                {
                    case ArgumentsConstants.Path:
                    {
                        yield return () => {}; 
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