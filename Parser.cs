using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BabyNI
{
    internal class Parser
    {
        readonly private static string  parserDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Parser",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private static Queue<string>    queue = new Queue<string>();
        private FileSystemWatcher       watcher;
        private static bool             isProcessing;

        public Parser()
        {
            // Watch for new incoming files
            watcher = new FileSystemWatcher();
            
            startWatcher(parserDirectory);

            processQueue();
        }

        private void startWatcher(string directory)
        {
            // Set directory to be watched
            watcher.Path = directory;

            // Enable the watcher
            watcher.EnableRaisingEvents = true;

            // Execute a function when a new file is added using the added file name
            watcher.Created += (sender, e) => addToQueue(directory, e.Name!);

            Console.WriteLine("Parser is up and running! :)\n");
        }

        private void addToQueue(string directory, string fileName)
        {
            bool isReady = false;

            // Wait for it to download
            isReady = isFileReady(Path.Combine(directory, fileName));

            // check if the queue contains the item, if item is not present add it to the queue
            if (!queue.Contains(fileName) && isReady)
            {
                queue.Enqueue(fileName);
            }

            if (!isProcessing)
            {
                processQueue();
            }
        }

        private void processQueue()
        {
            while (queue.Count != 0)
            {
                processItems();

                queue.Dequeue();
                
                //Console.WriteLine($"{queue.Count} items left in queue.\n");
            }
        }

        private void processItems()
        {
            // Start the parsing process
            isProcessing = true;
            parse(queue.Peek());
            isProcessing = false;
        }

        private bool isFileReady(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
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

        private void parse(string fileName)
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
