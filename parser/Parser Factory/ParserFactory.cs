using Parser.Parsers;

namespace Parser.Factory
{
    public class ParserFactory : IParserFactory
    {
        public RadioLinkParser CreateRadioLinkParser (string fileName)
        {
            return new RadioLinkParser(fileName);
        }

        public RFInputParser CreateRFInputParser (string fileName)
        {
            return new RFInputParser(fileName);
        }
    }
}
