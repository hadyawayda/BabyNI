using Loader.Loaders;

namespace Loader.Factory
{
    public interface ILoaderFactory
    {
        DbLoader CreateLoader(string fileName);
    }
}
