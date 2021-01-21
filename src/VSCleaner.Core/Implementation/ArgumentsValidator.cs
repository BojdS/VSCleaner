using System.Collections.Generic;
using System.Linq;
using VSCleaner.Core.Contracts;
using static VSCleaner.Core.Constants.VsCleanerConstants;

namespace VSCleaner.Core.Implementation
{
    public class ArgumentsValidator : IArgumentsValidator
    {
        public string ErrorMessage { private set; get; } = string.Empty;

        public bool ValidateArguments(ICollection<string> arguments)
        {
            if (arguments.Count == 0)
            {
                ErrorMessage = ValidationConstants.ArgumentsEmptyError;
                return false;
            }

            if (arguments.Count % 2 != 0)
            {
                ErrorMessage = ValidationConstants.ArgumentsWithoutValueError;
                return false;
            }
            
            if (!arguments.Contains(ArgumentsConstants.Path))
            {
                ErrorMessage = ValidationConstants.PathAbsentError;
                return false;
            }
            
            var argumentKeys = arguments
                .Where((_, i) => i % 2 == 0)
                .ToArray();

            if (argumentKeys.Any(x => !x.StartsWith("-")))
            {
                ErrorMessage = ValidationConstants.ArgumentPrefixError;
                return false;
            }
            
            if (!argumentKeys.Distinct().SequenceEqual(argumentKeys))
            {
                ErrorMessage = arguments
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key)
                    .Aggregate(ValidationConstants.ArgumentsDuplicatedError, (x, y) => $"{x}, {y}");
                    
                return false;
            }

            return true;
        }
    }
}