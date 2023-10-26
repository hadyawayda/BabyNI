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
        readonly static string  parserDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Parser",
                                radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        static Queue<string> queue = new Queue<string>();
        static bool isProcessing;

        static void Main()
        {
            startWatcher();

            processQueue();

            Console.ReadKey();
        }

        private static void startWatcher()
        {
            // Watch for new incoming files
            FileSystemWatcher watcher = new FileSystemWatcher();

            // Set directory to be watched
            watcher.Path = parserDirectory;

            // Execute a function when a new file is added using the added file name
            watcher.Created += (sender, e) =>
            {
                if (e.Name != null) addToQueue(e.Name);
                Console.WriteLine("***New***");
                Console.WriteLine($"{e.Name} has been queued and ready to be parsed.");
            };

            // Enable the watcher
            watcher.EnableRaisingEvents = true;

        }
        private static void addToQueue(string fileName)
        {
            bool isReady = false;

            // Wait for it to download
            isReady = isFileReady(Path.Combine(parserDirectory, fileName));

            // check if the queue contains the item, if item is not present add it to the queue
            if (!queue.Contains(fileName))
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
            parse(queue.Dequeue());
            isProcessing = false;
        }

        private static bool isFileReady(string fileName)
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

        private static void parse(string fileName)
        {

        }
    }
}
