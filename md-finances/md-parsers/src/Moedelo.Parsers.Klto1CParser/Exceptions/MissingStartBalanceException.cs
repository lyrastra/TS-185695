using Moedelo.Parsers.Klto1CParser.Resources;
using System;

namespace Moedelo.Parsers.Klto1CParser.Exceptions
{
    public class MissingStartBalanceException : Exception
    {
        public MissingStartBalanceException(string section)
           : base($"Missing value of {Predicates.StartBalance} in {section}")
        {
        }
    }
}
