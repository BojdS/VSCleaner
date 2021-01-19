using System.Collections.Generic;

namespace VSCleaner.Core.Constants
{
    public static class VsCleanerConstants
    {
        public static class ArgumentsConstants
        {
            public const string Path = "-p";
        }
        
        public static class ValidatorConstants
        {
            public const string ArgumentsEmptyError = "Arguments could be empty";
            public const string PathAbsentError = "Path -p is required";
            public const string ArgumentsWithoutValueError = "All arguments should have value";
            public const string ArgumentPrefixError = "All arguments should start with prefix -";
        }
    }
}