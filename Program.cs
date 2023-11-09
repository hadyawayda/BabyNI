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
