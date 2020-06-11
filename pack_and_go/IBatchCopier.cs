namespace PackAndGoPlugin
{
    using Elektrobit.Guide.Utilities;

    public interface IBatchCopier
    {
        void CopyTo(string targetDirectory);
    }
}