namespace PackAndGoPlugin
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Threading.Tasks;

    using Elektrobit.Guide.Scheduler;
    using Elektrobit.Guide.Studio.Exporter;
    using Elektrobit.Guide.Studio.Model.Elements;
    using Elektrobit.Guide.Studio.Workbench.ViewModels;
    using Elektrobit.Guide.Ui.Tools;
    using Elektrobit.Guide.Ui.ViewModels;
    using Elektrobit.Guide.Utilities;

    using Resources = PackAndGoPlugin.Properties.Resources;

    [Export(typeof(IPackAndGo))]
    public class PackAndGo : IPackAndGo
    {
        private readonly IFileDialogManager _fileDialogManager;

        private readonly IFileService _fileService;

        private readonly IModelExporter _modelExporter;

        private readonly IGtfCopier _gtfCopier;

        private readonly IBatchCopier _batchCopier;

        private readonly IZipper _zipper;

        private readonly ITaskSchedulerProvider _schedulerProvider;

        private readonly IMonitorCopier _monitorCopier;

        [ImportingConstructor]
        public PackAndGo(
            IFileDialogManager fileDialogManager,
            IFileService fileService,
            IModelExporter modelExporter,
            IGtfCopier gtfCopier,
            IBatchCopier batchCopier,
            IZipper zipper,
            IMonitorCopier monitorCopier,
            ITaskSchedulerProvider schedulerProvider
            )
        {
            _fileDialogManager = fileDialogManager;
            _fileService = fileService;
            _modelExporter = modelExporter;
            _gtfCopier = gtfCopier;
            _batchCopier = batchCopier;
            _zipper = zipper;
            _monitorCopier = monitorCopier;
            _schedulerProvider = schedulerProvider;
        }

        public async Task Run( (IWorkbenchViewModel workbench, bool useMonitor) param)
        {            
            var workbench = param.workbench;
            var projectContext = workbench.ProjectContext;       

            var dialog = _fileDialogManager.CreateSaveFileDialog(
                Resources.SaveDialogTitle,
                projectContext.Location,
                new FileDialogFilter(Resources.ZipFile, "*.zip"),
                Resources.ZipExtension,
                projectContext.Name);

            var result = dialog.ShowDialog();
            if (result == FileDialogResult.Cancel)
            {
                return;
            }

            try
            {
                workbench.SetBusy(true);
                //TODO Replace useMonitor varialble by method argument
                await _schedulerProvider.PoolScheduler.InvokeAsync(()=>ExportToZip(projectContext, dialog, param.useMonitor));
                ShowEventToastNotification(workbench, Resources.SuccessMsg, ToastNotificationPriority.Success);
            }
            catch (ExportModelException e)
            {
                ShowEventToastNotification(workbench, e.Message, ToastNotificationPriority.Warning);
            }
            catch (DirectoryNotFoundException e)
            {
                ShowEventToastNotification(workbench, e.Message, ToastNotificationPriority.Warning);
            }
            finally
            {
                workbench.SetBusy(false);
            }
        }

        private void ExportToZip(IProjectContext projectContext, IFileDialog dialog, bool useMonitor)
        {
            var baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            using (var tempDirectory = _fileService.CreateTemporaryDirectory(baseDirectory))
            {
                var targetDirectory = FromUnc(tempDirectory);

                _modelExporter.ExportSimulation(projectContext, targetDirectory);
                _gtfCopier.CopyTo(targetDirectory);
                _batchCopier.CopyTo(targetDirectory);
                if (useMonitor)
                {
                    _monitorCopier.CopyTo(targetDirectory);
                }
                _zipper.ZipTo(targetDirectory, dialog.SelectedFileName);
            }
        }

        private static string FromUnc(ITemporaryDirectory tempDirectory)
        {
            var value = @"\\?\";
            return tempDirectory.Path.StartsWith(value) ? tempDirectory.Path.Substring(value.Length) : tempDirectory.Path;
        }

        private static void ShowEventToastNotification(IWorkbenchViewModel workbenchViewModel, string toastMsg, ToastNotificationPriority prio)
        {
            var toastNotification = new ToastNotification
            {
                Header = Resources.ApplicationName,
                Content = toastMsg,
                Priority = prio
            };

            workbenchViewModel.ToastNotifier.Show(toastNotification);
        }
    }
}