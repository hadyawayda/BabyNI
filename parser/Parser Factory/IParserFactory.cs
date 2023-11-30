using Parser.Parsers;

namespace Parser.Factory
{
    public interface IParserFactory
    {
        RadioLinkParser CreateRadioLinkParser (string fileName);
        RFInputParser CreateRFInputParser (string fileName);
    }
}
