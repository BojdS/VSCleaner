using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VSCleaner.Core.Contracts;

namespace VSCleaner.Core.Implementation
{
    public class DirectoryCleaner : IDirectoryCleaner
    {
        private readonly DirectoryInfo directoryInfo;
        private const string BinDirectoryName = "bin";
        private const string ObjDirectoryName = "obj";

        public DirectoryCleaner(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new ArgumentException($"Directory: {directoryPath} not exists");
            
            directoryInfo = new DirectoryInfo(directoryPath);
        }
        
        public IEnumerable<string> CleanDirectory()
        {
            if(!IsDirectoryCSharpProjectRelated())
                throw new ArgumentException(
                    $"Directory: {directoryInfo.FullName} not related to C# project or solution. It should contain sln or csproj file");

            var directoriesForDelete = GetDeleteSubdirectories(directoryInfo, new List<string>());

            directoriesForDelete.ForEach(Directory.Delete);
            
            return directoriesForDelete;
        }
        private bool IsDirectoryCSharpProjectRelated()
        {
            return directoryInfo
                .GetFiles()
                .Any(f =>
                {
                    var extension = f.Name.Split('.').Last().ToLower();
                    return extension == "csproj" || extension == "sln";
                });
        }
        
        private static bool IsDirectoryDeleteAble(string name) => 
            name == ObjDirectoryName 
            || name == BinDirectoryName;
        
        private static List<string> GetDeleteSubdirectories(DirectoryInfo dirInfo, List<string> directoriesToDelete)
        {
            var subdirectories = dirInfo.GetDirectories();
            var toDelete = subdirectories.Where(d => IsDirectoryDeleteAble(d.Name)).Select(d => d.FullName);
            directoriesToDelete.AddRange(toDelete);
            
            foreach (var info in subdirectories)
            {
                if(IsDirectoryDeleteAble(info.Name))
                    continue;
                
                directoriesToDelete.AddRange(GetDeleteSubdirectories(info, directoriesToDelete));
            }

            return directoriesToDelete;
        }
    }
}