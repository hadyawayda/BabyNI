using Vertica.Data.VerticaClient;

namespace BabyNI.Helpers
{
    internal class QueryFetcher
    {
        private VerticaCommand? query;
        private DBConnection connection;
        private List<string>? queries;

        public QueryFetcher(string filePath, bool toClose)
        {
            connection = new DBConnection();

            query = connection.command;

            try
            {
                queries = File.ReadAllText(filePath).Split(';').ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            processQueries();

            if (toClose)
            {
                connection.CloseConnection();
            }
        }

        public void processQueries()
        {
            for (int i = 0; i < queries!.Count - 1; i++)
            {
                try
                {
                    query!.CommandText = queries[i] + ';';

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
