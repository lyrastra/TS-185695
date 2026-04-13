using Moedelo.Parsers.Klto1CParser.Resources;
using System;

namespace Moedelo.Parsers.Klto1CParser.Exceptions
{
    public class MissingPeriodStartDateException : Exception
    {
        public MissingPeriodStartDateException()
            : base($"Missing value of {Predicates.StartDate}")
        {
        }

        public MissingPeriodStartDateException(string section)
           : base($"Missing value of {Predicates.StartDate} in {section}")
        {
        }
    }
}
