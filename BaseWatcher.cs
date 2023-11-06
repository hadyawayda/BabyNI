using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyNI
{
    public class BaseWatcher <T>
    {
        private static Queue<string>?   queue;
        private FileSystemWatcher       watcher;
        private Action<string>          process;
        private string                  directory;
        private bool                    isProcessing, isReady;

        public BaseWatcher(string folder, Action<string> processAction)
        {
            isProcessing = isReady = false;

            directory = folder;

            process = processAction;

            queue = new Queue<string>();

            watcher = new FileSystemWatcher(directory);

            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Milestone 1 Reached!, {directory}");

            watcher.Created += (sender, e) => 
            {

                try
                {
                    Console.WriteLine($"Milestone 2 Reached!, new file detected: {e.Name}");

                    addToQueue(e.Name!);
                }
                catch (Exception f) 
                {
                    Console.WriteLine(f.Message);
                }
            };

            processQueue();
        }

        private void addToQueue(string fileName)
        {

            isReady = false;

            // Wait for it to download
            isReady = isFileReady(Path.Combine(directory, fileName));
            Console.WriteLine($"Milestone 3 Reached!, new file detected: {fileName}\n");

            // check if the queue contains the item, if item is not present add it to the queue
            if (!queue!.Contains(fileName) && isReady)
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
            while (queue!.Count != 0)
            {
                isProcessing = true;
                process(queue.Peek());
                queue.Dequeue();
                isProcessing = false;
                //Console.WriteLine($"{queue.Count} items left in queue.\n");
            }
        }

        private bool isFileReady(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (File.OpenRead(filePath))
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
                return isFileReady(filePath);
            }
        }
    }
}