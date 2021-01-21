using System;
using System.Collections.Generic;
using VSCleaner.Core.Contracts;
using static VSCleaner.Core.Constants.VsCleanerConstants;

namespace VSCleaner.Core.Implementation
{
    public class ArgumentsResolver : IArgumentsResolver
    {
        private readonly IDirectoryCleaner directoryCleaner;
        
        public ArgumentsResolver (IDirectoryCleaner directoryCleaner)
        {
            this.directoryCleaner = directoryCleaner;
        }
        
        public List<Func<IEnumerable<string>>> ProcessArguments(IDictionary<string, string> argumentsDictionary)
        {
            if (argumentsDictionary.Count == 0)
            {
                throw new ArgumentException(ValidationConstants.ArgumentsEmptyError);
            }
                
            var result = new List<Func<IEnumerable<string>>>();
            
            foreach (var (key, value) in argumentsDictionary)
            {
                switch (key)
                {
                    case ArgumentsConstants.Path:
                    {
                        result.Add(() => directoryCleaner.CleanDirectory(value));
                        break;
                    }
                    default:
                        throw new ArgumentException($"Invalid key: {key}");
                }
            }

            return result;
        }
    }
}