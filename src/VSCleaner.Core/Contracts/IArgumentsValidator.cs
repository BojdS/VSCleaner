using System.Collections.Generic;

namespace VSCleaner.Core.Contracts
{
    public interface IArgumentsValidator
    {
        string ErrorMessage { get; }
        bool ValidateArguments(ICollection<string> arguments);
    }
}