using System.Text.RegularExpressions;
using BabyNI.Helpers;

namespace BabyNI.Watching
{
    public class Watcher
    {
        readonly private static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$",
                                        parsed = Path.Combine(rootDirectory, "Parser"),
                                        backup = Path.Combine(rootDirectory, "Archive");
        private BaseWatcher watcher;
        private HashSet<string> logs = new HashSet<string>();
        private bool isProcessable;

        public Watcher()
        {
            watcher = new BaseWatcher(rootDirectory, process);
        }

        private void process(string fileName)
        {
            isProcessable = false;

            isProcessable = isFileProcessable(fileName);

            moveFiles(fileName, isProcessable);
        }

        private bool isFileProcessable(string fileName)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern) || Regex.IsMatch(fileName, RFInputPowerPattern))
            {
                if (!logs.Contains(fileName))
                {
                    logs.Add(fileName);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Sorry, file {fileName} has been processed already, skipping...");
                    File.Delete(Path.Combine(rootDirectory, fileName));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void moveFiles(string fileName, bool isProcessable)
        {
            if (isProcessable)
            {
                BaseWatcher.moveFiles(fileName, rootDirectory, backup, parsed);
            }
        }
    }
}

//  To be implemented:
//      1. File Logging (to the console, or maybe to an external file, or god forbid, to the UI)
//      2. Queueing functionality: process two files at the same time or more, i.e. store file names in an queue
//          and keep iterating over the queue and process them until the array is empty which means all files have been processed.
//          a. Maybe implement a limiter to the queue so it doesn't overload the system, and doesn't process ddos attacks.
//      3. Archiving functionality  Done
//      4. Multi-threading support
//      5. Add file size checker, if file is empty, drop the file, and if it is extremely big, drop it as well
//      6. Implement offline queue that stores item names in a .txt file on disk and reads file names on start
//      7. Scan watcher directory for new files while watcher was offline or didn't start yet