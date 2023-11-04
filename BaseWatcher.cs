using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyNI
{
    internal class BaseWatcher<T>
    {
        private readonly T I;
        private static bool isProcessing;
        private readonly string directory;
        private FileSystemWatcher watcher;
        private static Queue<string> queue = new Queue<string>();

        public BaseWatcher(T classInstance, string directory)
        { 
            I = classInstance;

            this.directory = directory;

            watcher = new FileSystemWatcher(directory);

            watcher.EnableRaisingEvents = true;

            watcher.Created += (sender, e) => addToQueue(directory, e.Name!);

            processQueue();
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
                isProcessing = true;
                I!.process(queue.Peek());
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


    }
}
