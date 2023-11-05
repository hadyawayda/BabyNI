using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BabyNI
{
    internal class Loader
    {
        readonly private static string  loaderDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader",
                                        createTablesScript = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Loader\Query Scripts\Create Tables.sql",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private BaseWatcher <Loader>    watcher;
        private StreamReader reader;
        private string?                 line;

        public Loader()
        {
            Console.WriteLine("Loader is up and running! :)\n");

            reader = new StreamReader(createTablesScript);

            createTables();

            watcher = new BaseWatcher <Loader> (loaderDirectory, process);
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
