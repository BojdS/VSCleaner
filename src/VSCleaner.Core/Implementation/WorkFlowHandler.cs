using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VSCleaner.Core.Contracts;

namespace VSCleaner.Core.Implementation
{
    [ExcludeFromCodeCoverage]
    public class WorkFlowHandler : IWorkFlowHandler
    {
        private WorkFlowHandler(){}
        public IEnumerable<string> Handle(string[] arguments)
        {
            var argumentsDictionary = GetArgumentsReader().ReadArgumentsToDictionary(arguments);
                
            var completionMessages = GetArgumentsResolver()
                .ProcessArguments(argumentsDictionary)
                .SelectMany(f => f());

            return completionMessages;
        }
        
        public static IWorkFlowHandler GetInstance() => new WorkFlowHandler();
        private static IArgumentsReader GetArgumentsReader() => new ArgumentsReader(new ArgumentsValidator());
        private static IArgumentsResolver GetArgumentsResolver() => new ArgumentsResolver(new DirectoryCleaner());
    }
}