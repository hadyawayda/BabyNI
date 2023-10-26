using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Watcher;

namespace BabyNI
{
    internal class BaseWatcher
    {
        private Program program;
        public BaseWatcher(string directory, Program program) {
            this.program = program;
            startWatcher(directory);
        }
        private static void startWatcher(string directory)
        {
            // Watch for new incoming files
            FileSystemWatcher watcher = new FileSystemWatcher();

            // Set directory to be watched
            watcher.Path = directory;

            // Execute a function when a new file is added using the added file name
            watcher.Created += (sender, e) =>
            {
                if (e.Name != null) program.addToQueue(e.Name);
            };

            // Enable the watcher
            watcher.EnableRaisingEvents = true;

        }
    }
}

//0 1 false 0
//0 4 false 0
//0 6 false 0
//0 7 true 0
//0 10 true 0
//1 4 true 0
//1 6 true 0
//1 7 true 0
//1 10 true 0
//4 6 true 0
//4 7 true 0
//4 10 true 0
//6 7 true 0
//6 10 true 0
//7 10 true 0