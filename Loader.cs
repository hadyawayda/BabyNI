using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vertica.Data.Internal.DotNetDSI;
using Vertica.Data.VerticaClient;

namespace BabyNI
{
    internal class Loader
    {
        readonly private static string          loaderDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader",
                                                createTablesScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader\Query Scripts\Create Tables.sql",
                                                radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                                RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private BaseWatcher <Loader>            watcher;
        private StreamReader                    reader;
        private string?                         line;
        private VerticaConnectionStringBuilder  builder;
        private string                          connectionString;
        private VerticaConnection               connection;
        private VerticaCommand                  command;
        public Loader()
        {
            Console.WriteLine("Loader is up and running! :)\n");

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

            command.CommandText = "CREATE TABLE TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RXLEVELBELOWTS1 VARCHAR,\r\n        RXLEVELBELOWTS2 VARCHAR,\r\n        MINRXLEVEL FLOAT,\r\n        MAXRXLEVEL FLOAT,\r\n        TXLEVELABOVETS1 VARCHAR,\r\n        MINTXLEVEL FLOAT,\r\n        MAXTXLEVEL FLOAT,\r\n        FAILUREDESCRIPTION VARCHAR,\r\n        LINK VARCHAR,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 1, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(HOUR, time_slice(TIME, 1, 'HOUR', 'END'), NOW()) >=24\r\n                THEN DATE_TRUNC('DAY', time_slice(TIME, 1, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 1, 'HOUR', 'END')\r\n                END;";

            Int32 rowsAdded = command.ExecuteNonQuery();

            Console.WriteLine($"{rowsAdded}");

            watcher = new BaseWatcher<Loader>(loaderDirectory, process);

            reader = new StreamReader(createTablesScript);

            createTables();
        }

        private void createTables()
        {
            while ( (line =  reader.ReadLine()) != null)
            {

            }
        }

        private void process(string fileName)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern))
            {
                RadioLinkLoader loader1 = new RadioLinkLoader(fileName);
            }

            else if (Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                RFInputLoader loader2 = new RFInputLoader(fileName);
            }
        }
    }
}