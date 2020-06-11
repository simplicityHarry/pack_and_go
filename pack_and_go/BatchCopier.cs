namespace PackAndGoPlugin
{
    using System.ComponentModel.Composition;

    using Elektrobit.Guide.Utilities;

    using PackAndGoPlugin.Properties;

    [Export(typeof(IBatchCopier))]
    public class BatchCopier : IBatchCopier
    {
        private readonly IFileService _fileService;

        [ImportingConstructor]
        public BatchCopier(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void CopyTo(string targetDirectory)
        {
            _fileService.CreateEmbeddedFile(targetDirectory, "simulation.bat", Resources.SimulationBatFile);
        }
    }
}
