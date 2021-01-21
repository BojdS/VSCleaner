using System;
using System.Linq;
using VSCleaner.Core.Implementation;

namespace VSCleaner.ConsoleApp
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var workflowHandler = WorkFlowHandler.GetInstance();
            try
            {
                workflowHandler.Handle(args).ToList().ForEach(Console.WriteLine);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("----------");
                Console.WriteLine($"Stack: {e.StackTrace}");
                return 1;
            }
        }
    }
}