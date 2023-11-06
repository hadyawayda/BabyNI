using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertica.Data.VerticaClient;

namespace BabyNI
{
    internal class RadioLinkLoader
    {
        private VerticaCommand query;

        public RadioLinkLoader(string fileName)
        {
            Console.WriteLine("Milestone 5 Reached!\n");

            Console.WriteLine(fileName);

            query = Connection.command!;

            query.CommandText = $"COPY TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER\r\n        FROM LOCAL 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File Drop-zone\\Loader\\{fileName}'\r\n        DELIMITER ','\r\n        SKIP 1\r\n        EXCEPTIONS 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File Drop-zone\\Loader\\Exceptions\\Radio-Link-Exceptions.csv'\r\n        REJECTED DATA 'C:\\Users\\User\\OneDrive - Novelus\\Desktop\\File Drop-zone\\Loader\\Exceptions\\Radio-Link-Rejections.csv';";

            Int32 rowsAdded = query.ExecuteNonQuery();

            Console.WriteLine($"Table 1 Created Successfully with {rowsAdded} rows.\n");
        }
    }
}
