using System;
using System.Collections.Generic;
using System.Linq;
using VSCleaner.Core.Contracts;
using VSCleaner.Core.Extensions;
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
                        result.Add(() =>
                        {
                            var deleteMessages = directoryCleaner
                                .CleanDirectory(value)
                                .ToArray();

                            return MessageConstants
                                .DirectoriesDeletedMessage
                                .Replace(KeysConstants.ReplaceKey, deleteMessages.Length.ToString())
                                .ToEnumerableOfOne()
                                .Concat(deleteMessages);
                        });
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