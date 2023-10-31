namespace BabyNI
{
    class Program
    {
        private Watcher watcher;
        private Parser parser;

        public Program()
        {
            watcher = new Watcher();
            parser = new Parser();
        }

        static void Main()
        {
            new Program();

            Console.ReadKey();
        }

    }
}
