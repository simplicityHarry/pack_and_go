namespace PackAndGoPlugin
{
    using System.Threading.Tasks;

    using Elektrobit.Guide.Studio.Workbench.ViewModels;

    internal interface IPackAndGo
    {
        Task Run((IWorkbenchViewModel workbench, bool useMonitor) param);
    }
}