namespace BabyNI
{
    class Program
    {
        // To-Do: Add a commit feature to update tables when a process has finished so we can have a backup plan in case of a failure

        private DBConnection connection;
        private Watcher watcher;
        private Parser parser;
        private Loader loader;

        public Program()
        {
            // Modify this to read from a .env file or global variables file instead of hardcoding this into C#
            connection = new DBConnection();

            watcher = new Watcher();
            
            parser = new Parser();
            
            loader = new Loader();

            Console.WriteLine("Everything ready!\n");
        }

        static void Main()
        {
            new Program();

            Console.ReadKey();
        }
    }
}
