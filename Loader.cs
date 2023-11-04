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
        readonly private static string parserDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone\Parser",
                                        radioLinkPowerPattern = @"^SOEM1_TN_RADIO_LINK_POWER_\d{8}_\d{6}\.txt$",
                                        RFInputPowerPattern = @"^SOEM1_TN_RFInputPower_\d{8}_\d{6}\.txt$";
        private static Queue<string> queue = new Queue<string>();
        private FileSystemWatcher watcher;
        private static bool isProcessing;

        public Loader()
        {
            watcher = new FileSystemWatcher();

            watcher.EnableRaisingEvents = true;
            
            Console.WriteLine("Parser is up and running! :)\n");

            watcher.Created += (sender, e) => addToQueue(e.Name!);

            processQueue();
        }

        private void addToQueue(string fileName)
        {
            bool isReady = false;

            // Wait for it to download
            isReady = isFileReady(Path.Combine(parserDirectory, fileName));

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
                isProcessing = true;
                process(queue.Peek());
                isProcessing = false;
                //Console.WriteLine($"{queue.Count} items left in queue.\n");
            }
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

            queue.Dequeue();
        }
    }
}
