namespace PackAndGoPluginTests
{
    using Elektrobit.Guide.Studio.Exporter;
    using Elektrobit.Guide.Studio.Model.Elements;
    using Elektrobit.Guide.Studio.Model.Elements.Profile;
    using Elektrobit.Guide.Utilities;

    using FakeItEasy;

    using NUnit.Framework;

    using PackAndGoPlugin;

    [TestFixture]
    public class ModelExporterShould
    {
        [Test]
        [TestCase("test")]
        [TestCase("temporary")]
        [TestCase("anotherTempDir")]
        public void ShouldExportWithCorrectConfiguration(string tempPath)
        {
            var exporter = A.Fake<IExporter>();
            var modelExporter = new ModelExporter(exporter);

            var projectContext = A.Fake<IProjectContext>();
            var directory = A.Fake<ITemporaryDirectory>();
            A.CallTo(() => directory.Path).Returns(tempPath);

            var simulationConfigProfile = A.Fake<IConfigurationProfile>();
            var project = A.Fake<IProject>();
            A.CallTo(() => project.SimulationModeConfigurationProfile).Returns(simulationConfigProfile);
            A.CallTo(() => projectContext.Project).Returns(project);


            modelExporter.ExportSimulation(projectContext, tempPath);

            A.CallTo(
                    () => exporter.ExportModel(
                        A<ExporterConfiguration>.That.Matches(
                            x => (x.TargetDir == $@"{tempPath}\model"
                                  && x.ConfigurationProfile == simulationConfigProfile))))
                .MustHaveHappened(1, Times.Exactly);
        }
    }
}
