using System.Text.RegularExpressions;

namespace BabyNI
{
    internal class Parser
    {
        readonly private static string  parserDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Parser",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private BaseWatcher             watcher;

        public Parser()
        {
            watcher = new BaseWatcher (parserDirectory, process);
        }

        private void process(string fileName)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern))
            {
                RadioLinkParser parser1 = new RadioLinkParser(fileName);
            }

            else if (Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                RFInputParser parser2 = new RFInputParser(fileName);
            }
        }
    }
}