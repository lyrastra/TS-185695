using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;
using System;

namespace Moedelo.Finances.Public.ClientData.Money.Table.Main
{
    public class MainMoneyTableBankBalanceResponseClientData
    {
        public long SourceId { get; set; }
        public decimal StartBalance { get; set; }
        public decimal EndBalance { get; set; }
        public decimal IncomingBalance { get; set; }
        //[JsonConverter(typeof(IsoDateConverter))]
        //public DateTime IncomingDate { get; set; }
        public decimal OutgoingBalance { get; set; }
        //[JsonConverter(typeof(IsoDateConverter))]
        //public DateTime OutgoingDate { get; set; }
    }
}