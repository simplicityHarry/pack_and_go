namespace PackAndGoPlugin
{
    using System;
    using System.Linq.Expressions;

    using Elektrobit.Guide.Utilities;

    public interface IZipper
    {
        void ZipTo(string sourceDirectory, string targetPath);
    }
}