namespace PackAndGoPluginTests
{
    using System;
    using System.Threading;

    using Elektrobit.Guide.Scheduler;
    using Elektrobit.Guide.Studio.Model.Elements;
    using Elektrobit.Guide.Studio.Workbench.ViewModels;
    using Elektrobit.Guide.Ui.Tools;
    using Elektrobit.Guide.Utilities;

    using FakeItEasy;

    using NUnit.Framework;

    using PackAndGoPlugin;

    using ExecutionMode = Elektrobit.Guide.Scheduler.ExecutionMode;

    [TestFixture]
    internal class PackAndGoShould
    {
        private static IProjectContext _projectContext;

        private static IWorkbenchViewModel _workbenchViewModel;

        private PackAndGo _packAndGo;

        private IFileDialogManager _fileDialogManager;

        private IModelExporter _modelExporter;

        private IGtfCopier _gtfCopier;

        private IBatchCopier _batchCopier;

        private IZipper _zipper;

        private IMonitorCopier _monitorCopier;

        private ITaskSchedulerProvider _schedulerProvider;

        [SetUp]
        public void SetUp()
        {
            _projectContext = A.Fake<IProjectContext>();

            _workbenchViewModel = A.Fake<IWorkbenchViewModel>();
            A.CallTo(() => _workbenchViewModel.ProjectContext).Returns(_projectContext);

            _fileDialogManager = A.Fake<IFileDialogManager>();
            _modelExporter = A.Fake<IModelExporter>();
            _gtfCopier = A.Fake<IGtfCopier>();
            _batchCopier = A.Fake<IBatchCopier>();
            _zipper = A.Fake<IZipper>();
            _monitorCopier = A.Fake<IMonitorCopier>();
            var fileService = A.Fake<IFileService>();

            _schedulerProvider = A.Fake<ITaskSchedulerProvider>();
            A.CallTo(
                () => _schedulerProvider.PoolScheduler.InvokeAsync(
                    A<Action>._,
                    A<CancellationToken>._,
                    A<ExecutionMode>._)).Invokes(oc => oc.GetArgument<Action>(0)());

            _packAndGo = new PackAndGo(_fileDialogManager, fileService, _modelExporter, _gtfCopier, _batchCopier, _zipper, _monitorCopier, _schedulerProvider);
        }

        [Test]
        public void OpenSaveDialogWithCorrectNames()
        {
            _packAndGo.Run((_workbenchViewModel, false));

            A.CallTo(
                () => _fileDialogManager.CreateSaveFileDialog(
                    A<string>._,
                    _projectContext.Location,
                    A<FileDialogFilter>._,
                    ".zip",
                    _projectContext.Name)).MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public void CallFunctionalityInCorrectOrder()
        {
            var fileDialog = A.Fake<IFileDialog>();
            A.CallTo(() => fileDialog.ShowDialog()).Returns(FileDialogResult.Ok);
            A.CallTo(
                () => _fileDialogManager.CreateSaveFileDialog(
                    A<string>._,
                    A<string>._,
                    A<FileDialogFilter>._,
                    A<string>._,
                    A<string>._)).Returns(fileDialog);

            _packAndGo.Run((_workbenchViewModel, false));

            A.CallTo(
                    () => _fileDialogManager.CreateSaveFileDialog(
                        A<string>._,
                        A<string>._,
                        A<FileDialogFilter>._,
                        A<string>._,
                        A<string>._)).MustHaveHappened(1, Times.Exactly)
                .Then(
                    A.CallTo(() => _modelExporter.ExportSimulation(_projectContext, A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo(() => _gtfCopier.CopyTo(A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo(() => _batchCopier.CopyTo(A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo(() => _zipper.ZipTo(A<string>._, A<string>._)).MustHaveHappened(1, Times.Exactly));
        }

        [Test]
        public void CallFunctionalityWithMonitorInCorrectOrder()
        {
            var fileDialog = A.Fake<IFileDialog>();
            A.CallTo(() => fileDialog.ShowDialog()).Returns(FileDialogResult.Ok);
            A.CallTo(
                () => _fileDialogManager.CreateSaveFileDialog(
                    A<string>._,
                    A<string>._,
                    A<FileDialogFilter>._,
                    A<string>._,
                    A<string>._)).Returns(fileDialog);

            _packAndGo.Run((_workbenchViewModel, true));

            A.CallTo(
                    () => _fileDialogManager.CreateSaveFileDialog(
                        A<string>._,
                        A<string>._,
                        A<FileDialogFilter>._,
                        A<string>._,
                        A<string>._)).MustHaveHappened(1, Times.Exactly)
                .Then(
                    A.CallTo(() => _modelExporter.ExportSimulation(_projectContext, A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo(() => _gtfCopier.CopyTo(A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo(() => _batchCopier.CopyTo(A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo( ()=> _monitorCopier.CopyTo(A<string>._)).MustHaveHappened(1, Times.Exactly)).Then(
                    A.CallTo(() => _zipper.ZipTo(A<string>._, A<string>._)).MustHaveHappened(1, Times.Exactly));
        }

        [Test]
        public void NotCallFunctionalityWhenDialogIsCancelled()
        {
            var fileDialog = A.Fake<IFileDialog>();
            A.CallTo(() => fileDialog.ShowDialog()).Returns(FileDialogResult.Cancel);
            A.CallTo(
                () => _fileDialogManager.CreateSaveFileDialog(
                    A<string>._,
                    A<string>._,
                    A<FileDialogFilter>._,
                    A<string>._,
                    A<string>._)).Returns(fileDialog);

            _packAndGo.Run((_workbenchViewModel, false));


            A.CallTo(() => _modelExporter.ExportSimulation(_projectContext, A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _gtfCopier.CopyTo(A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _batchCopier.CopyTo(A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _zipper.ZipTo(A<string>._, A<string>._)).MustNotHaveHappened();
        }
    }
}
