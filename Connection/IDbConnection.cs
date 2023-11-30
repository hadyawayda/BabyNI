using Vertica.Data.VerticaClient;

namespace BabyAPI.Connection
{
    public interface IDbConnection
    {
        void openConnection();

        VerticaCommand QueryCommand();
    }
}