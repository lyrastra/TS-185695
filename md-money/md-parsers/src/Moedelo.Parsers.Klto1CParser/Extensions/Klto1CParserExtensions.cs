using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;

namespace Moedelo.Parsers.Klto1CParser.Extensions
{
    public static class Klto1CParserExtensions
    {
        public static MovementList Parse(byte[] file, ParseOptions options = ParseOptions.None)
        {
            return Business.Klto1CParser.Parse(file.Encode(), options);
        }
    }
}
