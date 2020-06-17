namespace PackAndGoPlugin
{
    using System.ComponentModel.Composition;
    using System.IO;

    using Elektrobit.Guide.Studio.Exporter;
    using Elektrobit.Guide.Studio.Model.Elements;
    using Elektrobit.Guide.Studio.Model.Elements.Profile;
    using Elektrobit.Guide.Utilities;

    [Export(typeof(IModelExporter))]
    public class ModelExporter : IModelExporter
    {
        private readonly IExporter _exporter;

        [ImportingConstructor]
        public ModelExporter(IExporter exporter)
        {
            _exporter = exporter;
        }

        public void ExportSimulation(IProjectContext projectContext, string targetDirectory)
        {
            var targetDir = Path.Combine(targetDirectory, "model");
            var configurationProfile = projectContext.Project.SimulationModeConfigurationProfile;
            var exportConfiguration = new ExporterConfiguration(projectContext, targetDir, configurationProfile);
            _exporter.ExportModel(exportConfiguration);
        }


    }
}