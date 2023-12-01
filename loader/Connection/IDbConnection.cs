using Vertica.Data.VerticaClient;

namespace Loader.Connection
{
    public interface IDbConnection
    {
        void openConnection();

        VerticaCommand QueryCommand();
    }
}