using System;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MainMoneyTableBankBalance
    {
        public long SourceId { get; set; }

        public decimal StartBalance { get; set; }
        public decimal EndBalance { get; set; }

        public decimal IncomingBalance { get; set; }
        //public DateTime IncomingDate { get; set; }

        public decimal OutgoingBalance { get; set; }
        //public DateTime OutgoingDate { get; set; }
    }
}