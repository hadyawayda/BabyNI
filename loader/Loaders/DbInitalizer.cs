using Loader.Connection;
using Vertica.Data.VerticaClient;

namespace Loader.Loaders
{
    public class DbInitializer : IDbInitalizer
    {
        readonly private static string      createTablesScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone\Loader\Query Scripts\Create Tables.sql";
        private VerticaCommand              query;
        private List<string>?               queries;
        private readonly IDbConnection      _connection;

        public DbInitializer(IDbConnection connection)
        {
            _connection = connection;

            query = _connection.QueryCommand();

            ProcessQueries();
        }

        public void ProcessQueries()
        {
            try
            {
                queries = File.ReadAllText(createTablesScript).Split(';').ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            for (int i = 0; i < queries!.Count - 1; i++)
            {
                try
                {
                    query.CommandText = queries[i] + ';';

                    query.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
