namespace PackAndGoPlugin
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Elektrobit.Guide.Utilities;
    using Resources = PackAndGoPlugin.Properties.Resources;

    [Export(typeof(IGtfCopier))]
    public class GtfCopier : IGtfCopier
    {
        private readonly IFileService _fileService;

        [ImportingConstructor]
        public GtfCopier(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void CopyTo(string targetDirectory)
        {
            var searchPath = Path.Combine("studio", "lib", "ui");
            var guideLocation = Assembly.GetExecutingAssembly().Location;
            var index = guideLocation.LastIndexOf(searchPath, StringComparison.InvariantCultureIgnoreCase);
            var baseDirectory = guideLocation.Substring(0, index);
            var gtfRelativePath = Path.Combine("platform", "win64", "bin");
            var gtfLocation = Path.Combine(baseDirectory, gtfRelativePath);

            GtfCopy(gtfLocation, Path.Combine(targetDirectory, "bin"));
        }

        private static void GtfCopy(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    Resources.ErrorTxt_SourceNotFound
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            var validExtensions = new[] { ".dll", ".exe" };
            //copy only dlls
            var files = dir.GetFiles().Where(x => validExtensions.Contains(x.Extension));
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }
        }
    }
}
