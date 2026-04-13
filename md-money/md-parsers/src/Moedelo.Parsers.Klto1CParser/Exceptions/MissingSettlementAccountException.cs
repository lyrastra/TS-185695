using Moedelo.Parsers.Klto1CParser.Resources;
using System;

namespace Moedelo.Parsers.Klto1CParser.Exceptions
{
    public class MissingSettlementAccountException : Exception
    {
        public MissingSettlementAccountException()
            : base($"Missing value of {Predicates.Settlement}")
        {
        }

        public MissingSettlementAccountException(string section)
            : base($"Missing value of {Predicates.Settlement} in {section}")
        {
        }
    }
}