using Aggregator.Connection;
using Vertica.Data.VerticaClient;

namespace Aggregator.Aggregation
{
    public class DbAggregator : IAggregator
    {
        readonly private static string      aggregationScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone\Aggregator\Aggregate Tables.sql";
        private VerticaCommand              query;
        private List<string>?               queries;
        private readonly IDbConnection      _connection;

        public DbAggregator(IDbConnection connection)
        {
            _connection = connection;

            query = _connection.QueryCommand();

            ProcessQueries();
        }

        public void ProcessQueries()
        {
            try
            {
                queries = File.ReadAllText(aggregationScript).Split(';').ToList();
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
