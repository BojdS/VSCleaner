using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VSCleaner.Core.Contracts;
using static VSCleaner.Core.Constants.VsCleanerConstants;

namespace VSCleaner.Core.Implementation
{
    public class DirectoryCleaner : IDirectoryCleaner
    {
        private DirectoryInfo DirectoryInfo { set; get; }
        public IEnumerable<string> CleanDirectory(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException(ValidationConstants.DirectoryNotExistsError.Replace(KeysConstants.ReplaceKey, path));

            DirectoryInfo = new DirectoryInfo(path);
            if(!IsDirectoryCSharpProjectRelated())
                
                throw new ArgumentException(
                    ValidationConstants.NotRelatedToProjectDirectoryError.Replace(KeysConstants.ReplaceKey, DirectoryInfo.FullName));

            var directoriesForDelete = new List<string>();
            SetDeleteSubdirectoriesList(DirectoryInfo, directoriesForDelete);

            foreach (var directory in directoriesForDelete)
            {
                Directory.Delete(directory, true);
            }
            
            return directoriesForDelete;
        }
        private bool IsDirectoryCSharpProjectRelated()
        {
            return DirectoryInfo
                .GetFiles()
                .Any(f =>
                {
                    var extension = f.Name.Split('.').Last().ToLower();
                    return extension == KeysConstants.CsFileExtension || extension == KeysConstants.SlnFileExtension;
                });
        }
        
        private static bool IsDirectoryDeleteAble(string name) => 
            name == KeysConstants.BinFolder 
            || name == KeysConstants.ObjFolder;
        
        private static void SetDeleteSubdirectoriesList(DirectoryInfo dirInfo, List<string> directoriesToDelete)
        {
            var subdirectories = dirInfo.GetDirectories();
            var toDelete = subdirectories
                .Where(d => IsDirectoryDeleteAble(d.Name))
                .Select(d => d.FullName);
            directoriesToDelete.AddRange(toDelete);
            
            var notForDelete = subdirectories.Where(d => !IsDirectoryDeleteAble(d.Name));
            
            foreach (var info in notForDelete)
            {
                SetDeleteSubdirectoriesList(info, directoriesToDelete);
            }
        }
    }
}