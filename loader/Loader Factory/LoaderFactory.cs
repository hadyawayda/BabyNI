using Loader.Connection;
using Loader.Loaders;

namespace Loader.Factory
{
    public class LoaderFactory : ILoaderFactory
    {
        private readonly IDbConnection _connection;

        public LoaderFactory(IDbConnection connection)
        {
            _connection = connection;
        }

        public DbLoader CreateLoader(string fileName)
        {
            return new DbLoader(fileName, _connection);
        }
    }
}
