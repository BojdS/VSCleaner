using System;
using System.Collections.Generic;
using VSCleaner.Core.Contracts;

namespace VSCleaner.Core.Implementation
{
    public class ArgumentsReader : IArgumentsReader
    {
        private readonly IArgumentsValidator validator;

        public ArgumentsReader(IArgumentsValidator validator)
        {
            this.validator = validator;
        }
        public IDictionary<string, string> ReadArgumentsToDictionary(string[] arguments)
        {
            if (!validator.ValidateArguments(arguments))
                throw new ArgumentException(validator.ErrorMessage);
            
            var result = new Dictionary<string, string>();

            for (var i = 0; i < arguments.Length; i++)
            {
                if (i % 2 == 0)
                {
                    result.Add(arguments[i], arguments[i + 1]);
                }
            }
            
            return result;
        }
    }
}