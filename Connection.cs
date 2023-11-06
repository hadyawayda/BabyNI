using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Vertica.Data.Internal.DotNetDSI;
using Vertica.Data.VerticaClient;

namespace BabyNI
{
    internal class Connection
    {
        private VerticaConnectionStringBuilder  builder;
        private string                          connectionString;
        private VerticaConnection               connection;
        internal static VerticaCommand?         command;


        public Connection() 
        {
            Console.WriteLine("Database Connection Established!\n");

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

            command = connection.CreateCommand();
        }
    }
}
