using System.Text.RegularExpressions;
using BabyNI.Aggregation;
using BabyNI.Helpers;
using Vertica.Data.VerticaClient;

namespace BabyNI.Loading
{
    internal class Loader
    {
        // To-Do: Add a commit feature to update tables when a process has finished so we can have a backup plan in case of a failure

        readonly private static string      loaderDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone\Loader",
                                            loaderBackupDirectory = Path.Combine(loaderDirectory, "Processed"),
                                            createTablesScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone\Loader\Query Scripts\Create Tables.sql",
                                            RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.csv";
        private BaseWatcher                 watcher;
        private QueryFetcher                queryFetcher;
        private DBConnection?               connection;
        private Aggregator?                 aggregator;
        private VerticaCommand?             query;

        public Loader()
        {
            queryFetcher = new QueryFetcher(createTablesScript, true);

            watcher = new BaseWatcher(loaderDirectory, process);
        }

        private void process(string fileName)
        {
            string table = "";

            if (fileName.Contains("RADIO_LINK_POWER")) table = "TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER";
            else table = "TRANS_MW_ERC_PM_WAN_RFINPUTPOWER";

            connection = new DBConnection();

            query = connection.command;

            query!.CommandText = $"COPY {table}\r\n        FROM LOCAL 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File DropZone\\Loader\\{fileName}'\r\n        DELIMITER ','\r\n        SKIP 1\r\n        EXCEPTIONS 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File DropZone\\Loader\\Exceptions\\{fileName}-Exceptions.csv'\r\n        REJECTED DATA 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File DropZone\\Loader\\Exceptions\\{fileName}-Rejections.csv';";

            int rowsAdded = query.ExecuteNonQuery();

            Console.WriteLine($"\nTable Created Successfully with {rowsAdded} rows.\n");

            connection.CloseConnection();

            if (Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                aggregator = new Aggregator();
            }

            BaseWatcher.moveFiles(fileName, loaderDirectory, loaderBackupDirectory);
        }
    }
}