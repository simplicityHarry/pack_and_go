namespace PackAndGoPlugin
{
    using Elektrobit.Guide.Studio.Model.Elements;

    public interface IModelExporter
    {
        void ExportSimulation(IProjectContext projectContext, string targetDirectory);
    }
}