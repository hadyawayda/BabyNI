using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace BabyNI
{
    public class Watcher
    {
        readonly private static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private BaseWatcher <Watcher>   watcher;
        private HashSet<string>         FileList = new HashSet<string>();
        bool                            isProcessable;

        public Watcher ()
        {
            //Console.WriteLine("Watcher is up and running!\n");

            watcher = new BaseWatcher <Watcher> (rootDirectory, process);

            isProcessable = false;
        }

        private void process(string fileName)
        {
            isProcessable = false;

            // Authenticate txt file names, fetch their names and check if they're in the list of processed files.
            // If already present in the list of files, skip them, otherwise allow them to be processed.
            isProcessable = isFileProcessable(fileName);

            // Move files if they are processable
            moveFiles(fileName, isProcessable);
        }

        private bool isFileProcessable(string fileName)
        {
            if ( Regex.IsMatch(fileName, radioLinkPowerPattern) || Regex.IsMatch(fileName, RFInputPowerPattern) )
            {
                if (!FileList.Contains(fileName))
                {
                    FileList.Add(fileName);
                    //Console.WriteLine($"New file logged: {item}");
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

        // Maybe reference this method in a helper class and use method overloading to make it move files depending on parameters
        private void moveFiles(string fileName, bool isProcessable)
        {
            if (isProcessable)
            {
                // This method could make use of a queue system as well, but it's not that important right now.
                string path = Path.Combine(rootDirectory, fileName);
                string parsed = Path.Combine(rootDirectory, "Parser", fileName);
                string backup = Path.Combine(rootDirectory, "Archive", fileName);

                //  Use this for a dynamic file name
                //  string title = Regex.IsMatch(fileName, radioLinkPowerPattern) ? "RadioLink.txt" : Regex.IsMatch(fileName, RFInputPowerPattern) ? "RFInput.txt" : "";
                //  string parsed = Path.Combine(parserDirectory, title);

                if (File.Exists(backup) || File.Exists(parsed))
                {
                    File.Delete(backup);

                    File.Delete(parsed);
                }

                File.Copy(path, parsed);

                File.Move(path, backup);
            }
        }
    }
}

// Add functionality to ignore empty files
// ToDo:                        1. Implement SystemFileWatcher and then move on to the parser    Done
// ***Parser functionality***:  2. Also implement functionality to process two files at the same time or more, i.e. store file names in an array
//                                  and keep iterating over the names and process them until the array is empty which means all files have been processed. (rather implement step 5)    Done
//                              3. Log Files (possible ways: to the application memory, to a log file i.e. .txt file, or to the database)
//                              4. keep track of what has been pushed and what wasn't (done, no need to track what wasn't done), avoid duplicates! Unless entry failed, or file was corrupted!  Done
//                              5. What if the file size is big and needs time to download?     Done
// ***Parser functionality***:  6. Implement a queue to store and wait for new files to be processed by the parser.     Done
//                              7. Add file size checker, if file is empty, drop the file, and if it is extremely big, drop it as well

// To be implemented:
//          1. File Logging (to the console, or maybe to an external file, or god forbid, to the UI)
//          2. Queueing functionality: process two files at the same time or more, i.e. store file names in an queue
//              and keep iterating over the queue and process them until the array is empty which means all files have been processed.
//              a. Maybe implement a limiter to the queue so it doesn't overload the system, and doesn't process ddos attacks.
//          3. Archiving functionality  Done
//          4. Multi-threading support
// Explanation:     1. In case we have a file that was copying and we had the watcher process running on it, if we implement a queue, it would solve the problem of having to process it again, because it would be present in the queue and the watcher starts with the items that are present in the queue, so a general overview of the watcher, is that it watches for new files that enter the generic location, then adds them to the queue and starts processing the items in the queue first, so if the queue was empty when our new file entered the directory, it would be added to the queue and then the file would be processed from the queue...
// l watcher bya3mol execute lal addToQueue method(), wel addToQueue() method bta3mol execute lal processQueue() method if we're processing anything.
// l watcher bikeb esem l file bel addToQueue() method, wel addToQueue() method betshouf eza mawjoud bel queue, w 3a hal ases bet7aded eza eja dawro. In case eja dawro, bta3mol run lal processQueue() method w betshil esem l file men l queue.

// loaderDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\BabyNI\Loader",