namespace Parser.Watcher
{
    public class BaseWatcher : IDisposable
    {
        private static Queue<string>? queue;
        private FileSystemWatcher watcher;
        private Action<string> process;
        private string directory;
        private bool isProcessing, isReady;

        public BaseWatcher(string folder, Action<string> processAction)
        {
            isProcessing = isReady = false;

            directory = folder;

            process = processAction;

            queue = new();

            watcher = new FileSystemWatcher(directory);

            watcher.EnableRaisingEvents = true;

            watcher.Created += (sender, e) => addToQueue(e.Name!);

            //watcher.Changed += (sender, e) => addToQueue(e.Name!);

            processQueue();
        }

        private void addToQueue(string fileName)
        {
            isReady = false;

            // Wait for it to download
            isReady = isFileReady(Path.Combine(directory, fileName));

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

        // switch to while loop
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
                {
                    return false;
                }
            }

            catch (Exception)
            {
                Thread.Sleep(100);

                //Console.WriteLine("How much longer do I have to wait???");

                return isFileReady(filePath);
            }
        }

        public void Dispose()
        {
            watcher.Dispose();
        }
    }
}