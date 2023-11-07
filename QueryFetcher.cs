using Vertica.Data.VerticaClient;

namespace BabyNI
{
    internal class QueryFetcher
    {
        private VerticaCommand query;
        private List<string>? queries;

        public QueryFetcher(string filePath) 
        {
            query = DBConnection.command!;

            try
            {
                queries = File.ReadAllText(filePath).Split(';').ToList();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found: " + filePath);
            }
            catch (IOException)
            {
                Console.WriteLine($"Could not find {filePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            processQueries();
        }

        public void processQueries()
        {
            for ( int i = 0; i < queries!.Count - 1; i++ )
            {
                //Console.WriteLine($"\nCurrently executing the following query:\n{queries[i] + ';'}\n");

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
