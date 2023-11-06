namespace BabyNI
{
    internal class ParserForRadioLinkFiles
    {
        string? singleLine;
        StreamReader reader;
        StreamWriter writer;
        string parserContainer = @"C:\Baby NI\5-Parser container";
        string parsingOutput = @"C:\Baby NI\6-Parsing Result";
        bool header = true;
        string inputFile;
        string outputFile;

        public ParserForRadioLinkFiles(string fileName)
        {
            inputFile = Path.Combine(parserContainer, fileName);

            outputFile = Path.Combine(parsingOutput, fileName);

            reader = new StreamReader(inputFile);

            writer = new StreamWriter(outputFile);

            Initialize();
        }

        public void Initialize()
        {
            while (((singleLine = reader.ReadLine()!) != null))
            {
                if (header)
                {
                    headerToOutput();
                    continue;
                }

                lineToOutput();

            }

        }

        public void headerToOutput()
        {

        }

        public void lineToOutput()
        {

        }
    }
}
