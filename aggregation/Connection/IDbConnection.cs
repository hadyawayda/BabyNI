using Vertica.Data.VerticaClient;

namespace Aggregator.Connection
{
    public interface IDbConnection
    {
        void openConnection();

        VerticaCommand QueryCommand();
    }
}