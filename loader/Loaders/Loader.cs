using Loader.Connection;
using Vertica.Data.VerticaClient;

namespace Loader.Loaders
{
    public class DbLoader
    {
        // To-Do: Add a commit feature to update tables when a process has finished so we can have a backup plan in case of a failure
        private VerticaCommand              query;
        private readonly IDbConnection      _connection;

        public DbLoader(string fileName, IDbConnection connection)
        {
            _connection = connection;

            query = _connection.QueryCommand();

            process(Path.GetFileNameWithoutExtension(fileName) + ".csv");
        }

        private void process(string fileName)
        {
            string table = "";

            if (fileName.Contains("RADIO_LINK_POWER")) table = "TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER";

            else table = "TRANS_MW_ERC_PM_WAN_RFINPUTPOWER";

            query.CommandText = $"COPY {table} FROM LOCAL 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File DropZone\\Parser\\{fileName}'\r\n        DELIMITER ','\r\n        SKIP 1\r\n        EXCEPTIONS 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File DropZone\\Loader\\Exceptions\\{fileName}-Exceptions.csv'\r\n        REJECTED DATA 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File DropZone\\Loader\\Exceptions\\{fileName}-Rejections.csv';";

            int rowsAdded = query.ExecuteNonQuery();

            Console.WriteLine($"Table Created Successfully with {rowsAdded} rows.\n");
        }
    }
}