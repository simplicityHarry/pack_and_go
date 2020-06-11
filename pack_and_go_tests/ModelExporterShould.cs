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
        public void ShouldExportToCorrectTemporaryFolder(string tempPath)
        {
            var exporter = A.Fake<IExporter>();
            var modelExporter = new ModelExporter(exporter);

            var projectContext = A.Fake<IProjectContext>();
            var directory = A.Fake<ITemporaryDirectory>();
            A.CallTo(() => directory.Path).Returns(tempPath);
            modelExporter.ExportSimulation(projectContext, tempPath);

            A.CallTo(
                () => exporter.ExportModel(
                    projectContext,
                    A<string>.That.Matches(x => x == $@"{tempPath}\model"),
                    A<IConfigurationProfile>._)).MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public void ShouldUseCorrectProfileForExport()
        {
            var exporter = A.Fake<IExporter>();
            var modelExporter = new ModelExporter(exporter);


            var simulationConfigProfile = A.Fake<IConfigurationProfile>();
            var project = A.Fake<IProject>();
            A.CallTo(() => project.SimulationModeConfigurationProfile).Returns(simulationConfigProfile);


            var projectContext = A.Fake<IProjectContext>();
            A.CallTo(() => projectContext.Project).Returns(project);

            var directory = A.Dummy<string>();
            modelExporter.ExportSimulation(projectContext, directory);

            A.CallTo(
                () => exporter.ExportModel(
                    projectContext,
                    A<string>._,
                    A<IConfigurationProfile>.That.Matches(x=> x == simulationConfigProfile))).MustHaveHappened(1, Times.Exactly);
        }
    }
}
