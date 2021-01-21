namespace VSCleaner.Core.Constants
{
    public static class VsCleanerConstants
    {
        public static class ArgumentsConstants
        {
            public const string Path = "-p";
        }
        
        public static class ValidationConstants
        {
            public const string ArgumentsEmptyError = "Arguments could be empty";
            public const string PathAbsentError = "Path -p is required";
            public const string ArgumentsWithoutValueError = "All arguments should have value";
            public const string ArgumentPrefixError = "All arguments should start with prefix -";
            public const string ArgumentsDuplicatedError = "Arguments contains duplicated values: ";
            public const string DirectoryNotExistsError = "Directory: #replaceKey# not exists";
            public const string NotRelatedToProjectDirectoryError =
                "Directory: ##replaceKey# not related to C# project or solution. It should contain sln or csproj file";
        }
        public static class KeysConstants
        {
            public const string ReplaceKey = "#replaceKey#";
            public const string BinFolder = "bin";
            public const string ObjFolder = "obj";
            public const string CsFileExtension = "csproj";
            public const string SlnFileExtension = "sln";
        }
    }
}