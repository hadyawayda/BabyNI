using Vertica.Data.VerticaClient;

namespace BabyNI.Helpers
{
    internal class DBConnection
    {
        private VerticaConnectionStringBuilder  builder;
        private string                          connectionString;
        private VerticaConnection               connection;
        internal VerticaCommand                 command;

        public DBConnection()
        {
            // Modify this to read from a .env file or global variables file instead of hardcoding this into C#
            builder = new VerticaConnectionStringBuilder
            {
                Host = "10.10.4.231",
                Database = "test",
                User = "bootcamp6",
                Password = "bootcamp62023"
            };

            connectionString = builder.ToString();

            connection = new VerticaConnection(connectionString);

            connection.Open();

            Console.WriteLine("Database Connection Established!\n");

            command = connection.CreateCommand();
        }

        internal void CloseConnection()
        {
            connection.Close();

            Console.WriteLine("Database Connection Terminated!\n");
        }
    }
}
