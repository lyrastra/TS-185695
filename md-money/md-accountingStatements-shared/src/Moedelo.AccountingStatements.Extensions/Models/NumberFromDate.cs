using System;

namespace Moedelo.AccountingStatements.Extensions.Models
{
    internal struct NumberFromDate
    {
        private string stamp;
        private int counter;
        
        public NumberFromDate(DateTime onDate, int counter)
        {
            stamp = $"{onDate:MMdd}{DateTime.Now:HHmmssfff}";
            this.counter = counter;
        }

        public static NumberFromDate operator ++(NumberFromDate obj)
        {
            obj.counter += 1;
            return obj;
        }

        public override string ToString()
        {
            return counter > 0 ? $"{stamp}/{counter}" : $"{stamp}";
        }
    }
}