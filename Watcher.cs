using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BabyNI;

namespace Watcher
{
    class Program
    {
        readonly static string rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone",
                parserDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Parser",
                backupDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Archive",
                radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        static HashSet<string> RadioLinkPowerList = new HashSet<string>();
        static HashSet<string> RFInputPowerList = new HashSet<string>();
        static Queue<string> queue = new Queue<string>();
        static bool isProcessing;

        static void Main()
        {
            BaseWatcher watcher = new BaseWatcher(parserDirectory);

            processQueue();

            Console.ReadKey();
        }

        private static void startWatcher()
        {
            // Watch for new incoming files
            FileSystemWatcher watcher = new FileSystemWatcher();

            // Set directory to be watched
            watcher.Path = rootDirectory;

            // Execute a function when a new file is added using the added file name
            watcher.Created += (sender, e) =>
            {
                if (e.Name != null) addToQueue(e.Name);
            };

            // Enable the watcher
            watcher.EnableRaisingEvents = true;

        }

        private static void addToQueue(string fileName)
        {
            bool isProcessable = false;
            bool isReady = false;

            // Wait for it to download
            isReady = isFileReady(Path.Combine(rootDirectory, fileName));

            // Authenticate txt file names, fetch their names and check if they're in the list of processed files.
            // If already present in the list of files, skip them, otherwise allow them to be processed.
            isProcessable = isFileProcessable(fileName, isReady);

            // check if the queue contains the item, if item is not present add it to the queue
            if (!queue.Contains(fileName) && isProcessable)
            {
                queue.Enqueue(fileName);
            }
            
            if (!isProcessing)
            {
                processQueue();
            }
        }

        private static void processQueue()
        {
            while (queue.Count != 0)
            {
                processItems();
            }
        }

        private static void processItems()
        {
            // Start the parsing process
            isProcessing = true;
            moveFiles(queue.Dequeue());
            isProcessing = false;
        }

        private static bool isFileReady(string fileName)
        {
            try
            {
                if (File.Exists(fileName)) { 
                    using (File.OpenRead(fileName))
                    {
                        return true;
                    }
                }
                else
                    return false;
            }
            catch (Exception)
            {
                Thread.Sleep(100);
                //Console.WriteLine("How much longer do I have to wait???");
                return isFileReady(fileName);
            }
        }

        private static bool isFileProcessable(string fileName, bool ready)
        {
            if (Regex.IsMatch(fileName, radioLinkPowerPattern) && ready)
            {   
                if (!RadioLinkPowerList.Contains(fileName))
                {
                    RadioLinkPowerList.Add(fileName);
                    foreach (string item in RadioLinkPowerList) {
                        Console.WriteLine($"New file logged: {item}");
                    };
                    return true;
                }
                else
                {
                    Console.WriteLine($"Sorry, '{fileName}' has been processed already, skipping...");
                    File.Delete(Path.Combine(rootDirectory, fileName));
                    return false;
                }
            }
            else if (Regex.IsMatch(fileName, RFInputPowerPattern) && ready)
            {
                if (!RFInputPowerList.Contains(fileName))
                {
                    RFInputPowerList.Add(fileName);
                    foreach (string item in RFInputPowerList)
                    {
                        Console.WriteLine($"New file logged: {item}");
                    };
                    return true;
                }
                else
                {
                    Console.WriteLine($"Sorry, '{fileName}' has been processed already, skipping...");
                    File.Delete(Path.Combine(rootDirectory, fileName));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static void moveFiles(string fileName)
        {
            // This method could make use of a queue system as well, but it's not that important right now.
            string path = Path.Combine(rootDirectory, fileName);
            string backup = Path.Combine(backupDirectory, fileName);
            string parsed = Path.Combine(parserDirectory, fileName);

            //  use this for a dynamic file name
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

// Add functionality to ignore empty files
// ToDo:                        1. Implement SystemFileWatcher and then move on to the parser    Done
// ***Parser functionality***:  2. Also implement functionality to process two files at the same time or more, i.e. store file names in an array
//                                  and keep iterating over the names and process them until the array is empty which means all files have been processed. (rather implement step 5)    Done
//                              3. Log Files (possible ways: to the application memory, to a log file i.e. .txt file, or to the database)
//                              4. keep track of what has been pushed and what wasn't (done, no need to track what wasn't done), avoid duplicates! Unless entry failed, or file was corrupted!  Done
//                              5. What if the file size is big and needs time to download?     Done
// ***Parser functionality***:  6. Implement a queue to store and wait for new files to be processed by the parser.     Done

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