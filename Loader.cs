using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vertica.Data.Internal.DotNetDSI;
using Vertica.Data.VerticaClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BabyNI
{
    //public class YourDbContext : DbContext
    //{
    //    public YourDbContext() : base("YourConnectionString")
    //    {
    //        // YourDbContext constructor with a connection string.
    //    }
    //}

    internal class Loader
    {
        // To-Do: Add a commit feature to update tables when a process has finished so we can have a backup plan in case of a failure

        readonly private static string          loaderDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader",
                                                createTablesScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader\Query Scripts\Create Tables.sql",
                                                radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.csv",
                                                RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.csv";
        private BaseWatcher <Loader>            watcher;
        //private StreamReader                    reader;
        private string?                         line;
        private VerticaCommand                  query;

        public Loader()
        {
            //Console.WriteLine("Loader is up and running! :)\n");

            query = Connection.command!;

            fetchQueries();

            //Int32 rowsAdded = command.ExecuteNonQuery();

            //Console.WriteLine($"{rowsAdded}");

            watcher = new BaseWatcher<Loader>(loaderDirectory, process);

            //reader = new StreamReader(createTablesScript);

            //createTables();
        }

        // To-Do:   Fetch queries one by one and run them, and in case they are dynamic queries, construct them properly and pass the correct dynamic values to them and run them.
        // Step 1:  Start by fetching and running hard-coded queries from an external file.

        //private string constructQuery()
        //{

        //    return "";
        //}

        private void fetchQueries()
        {
            //retrieveQueries();

            //foreach (string query in queryHolder)
            //{
            query.CommandText = "";

            query.CommandText = "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER;";

            query.ExecuteNonQuery();

            query.CommandText = "CREATE TABLE TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RXLEVELBELOWTS1 VARCHAR,\r\n        RXLEVELBELOWTS2 VARCHAR,\r\n        MINRXLEVEL FLOAT,\r\n        MAXRXLEVEL FLOAT,\r\n        TXLEVELABOVETS1 VARCHAR,\r\n        MINTXLEVEL FLOAT,\r\n        MAXTXLEVEL FLOAT,\r\n        FAILUREDESCRIPTION VARCHAR,\r\n        LINK VARCHAR,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 1, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(HOUR, time_slice(TIME, 1, 'HOUR', 'END'), NOW()) >=24\r\n                THEN DATE_TRUNC('DAY', time_slice(TIME, 1, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 1, 'HOUR', 'END')\r\n                END;";

            query.ExecuteNonQuery();

            query.CommandText = "DROP TABLE IF EXISTS TRANS_MW_ERC_PM_WAN_RFINPUTPOWER;";

            query.ExecuteNonQuery();

            query.CommandText = "CREATE TABLE TRANS_MW_ERC_PM_WAN_RFINPUTPOWER (\r\n        NETWORK_SID INTEGER,\r\n        DATETIME_KEY INTEGER,\r\n        NODENAME VARCHAR,\r\n        NEID FLOAT,\r\n        \"OBJECT\" VARCHAR,\r\n        \"TIME\" DATETIME,\r\n        \"INTERVAL\" INTEGER,\r\n        DIRECTION VARCHAR,\r\n        NEALIAS VARCHAR,\r\n        NETYPE VARCHAR,\r\n        RFINPUTPOWER FLOAT,\r\n        TID VARCHAR,\r\n        FARENDTID VARCHAR,\r\n        SLOT VARCHAR,\r\n        PORT VARCHAR\r\n        )\r\n        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1\r\n        PARTITION BY time_slice(TIME, 24, 'HOUR', 'END')\r\n        GROUP BY CASE\r\n                WHEN DATEDIFF(DAY, time_slice(TIME, 24, 'HOUR', 'END'), NOW()) >=7\r\n                THEN DATE_TRUNC('WEEK', time_slice(TIME, 24, 'HOUR', 'END'))\r\n                ELSE time_slice(TIME, 24, 'HOUR', 'END')\r\n                END;";

            query.ExecuteNonQuery();

            //}
        }

        //private void retrieveQueries()
        //{
        //    using (var context = new YourDbContext())
        //{
        //    // Define the path to your SQL queries file.
        //    string filePath = "queries.sql";

        //    try
        //    {
        //        // Read all lines from the file into an array.
        //        string[] queries = File.ReadAllLines(filePath);

        //        foreach (string query in queries)
        //        {
        //            // Execute the query using Entity Framework.
        //            var result = context.Database.SqlQuery<YourEntityType>(query).ToList();

        //            // Process the result as needed.
        //            foreach (var item in result)
        //            {
        //                Console.WriteLine($"Result: {item.PropertyName}");
        //            }
        //        }
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        Console.WriteLine("File not found: " + filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //    }
        //}
        //}

        //private void createTables()
        //{
        //    while ( (line =  reader.ReadLine()) != null)
        //    {

        //    }
        //}

        private void process(string fileName)
        {
            Console.WriteLine("Milestone 4 Reached!\n");
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