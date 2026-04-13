using System;

namespace Moedelo.Parsers.Klto1CParser.Enums
{
    [Flags]
    public enum ParseOptions : byte
    {
        None = 0,
        NoCheckStartBalance = 1,
    }
}