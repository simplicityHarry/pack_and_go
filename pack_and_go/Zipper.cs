namespace PackAndGoPlugin
{
    using System.ComponentModel.Composition;
    using System.IO;

    [Export(typeof(IZipper))]
    public class Zipper : IZipper
    {
        public void ZipTo(string sourceDirectory, string targetPath)
        {
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
       
            System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectory, targetPath);
        }
    }
}