using System.Text.RegularExpressions;

namespace BabyNI
{
    internal class Loader
    {
        // To-Do: Add a commit feature to update tables when a process has finished so we can have a backup plan in case of a failure
        // To-Do:   Fetch queries one by one and run them, and in case they are dynamic queries, construct them properly and pass the correct dynamic values to them and run them.
        // Step 1:  Start by fetching and running hard-coded queries from an external file.

        readonly private static string          loaderDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader",
                                                loaderBackupDirectory = Path.Combine(loaderDirectory, "Processed"),
                                                createTablesScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader\Query Scripts\Create Tables.sql",
                                                radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.csv",
                                                RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.csv";
        private BaseWatcher                     watcher;
        private QueryFetcher                    queryFetcher;
        //private Aggregator aggregator;

        public Loader()
        {
            queryFetcher = new QueryFetcher(createTablesScript);

            watcher = new BaseWatcher(loaderDirectory, process);

            //aggregator = new Aggregator();
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

            BaseWatcher.moveFiles(fileName, loaderDirectory, loaderBackupDirectory);
        }
    }
}