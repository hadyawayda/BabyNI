using Vertica.Data.VerticaClient;

namespace Aggregator.Connection
{
    public class DbConnection : IDbConnection, IDisposable
    {
        private VerticaConnection _connection;
        public DbConnection(VerticaConnection connection)
        {
            _connection = connection;

            openConnection();

            Console.WriteLine("Database Connection Established!\n");
        }
        public void openConnection()
        {
            _connection.Open();
        }

        public VerticaCommand QueryCommand()
        {
            return _connection.CreateCommand(); ;
        }

        public void Dispose()
        {
            _connection.Close();

            Console.WriteLine("Database Connection Terminated!\n");
        }
    }
}
