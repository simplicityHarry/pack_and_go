namespace PackAndGoPlugin
{
    using Elektrobit.Guide.Utilities;

    public interface IGtfCopier
    {
        void CopyTo(string targetDirectory);
    }
}