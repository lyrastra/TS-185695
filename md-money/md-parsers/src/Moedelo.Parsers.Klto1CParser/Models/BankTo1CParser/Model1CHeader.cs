using System;

namespace Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser
{
    public class Model1CHeader
    {
        public string BankName { get; set; }
        public string SettlementAccount { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal IncomingBalance { get; set; }
        public decimal OutgoingBalance { get; set; }
        public decimal IncomingSumm { get; set; }
        public decimal OutgoingSumm { get; set; }
    }
}