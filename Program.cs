namespace BabyNI
{
    class Program
    {
        private Watcher watcher;
        private Parser parser;
        private Loader loader;

        public Program()
        {
            watcher = new Watcher();
            parser = new Parser();
            loader = new Loader();
        }

        static void Main()
        {
            new Program();

            Console.ReadKey();
        }

    }
}
