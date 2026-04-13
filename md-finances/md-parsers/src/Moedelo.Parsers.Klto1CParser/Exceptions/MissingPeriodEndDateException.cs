using Moedelo.Parsers.Klto1CParser.Resources;
using System;

namespace Moedelo.Parsers.Klto1CParser.Exceptions
{
    public class MissingPeriodEndDateException : Exception
    {
        public MissingPeriodEndDateException()
            : base($"Missing value of {Predicates.EndDate}")
        {
        }

        public MissingPeriodEndDateException(string section)
            : base($"Missing value of {Predicates.EndDate} in {section}")
        {
        }
    }
}
