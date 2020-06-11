namespace PackAndGoPlugin
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Elektrobit.Guide.Utilities;
    using Resources = PackAndGoPlugin.Properties.Resources;

    [Export(typeof(IMonitorCopier))]
    public class MonitorCopier : IMonitorCopier
    {
        private readonly IFileService _fileService;

        [ImportingConstructor]
        public MonitorCopier(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void CopyTo(string targetDirectory)
        {            
            var searchPath = Path.Combine("studio", "lib", "ui");
            var guideLocation = Assembly.GetExecutingAssembly().Location;
            var index = guideLocation.LastIndexOf(searchPath, StringComparison.InvariantCultureIgnoreCase);
            var baseDirectory = guideLocation.Substring(0, index);
            var monitorRelativePath = Path.Combine("tools", "monitor");
            var monitorLocation = Path.Combine(baseDirectory, monitorRelativePath);

            DirectoryCopy(monitorLocation, Path.Combine(targetDirectory, "monitor"), true);
            
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    Resources.ErrorTxt_SourceNotFound
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
