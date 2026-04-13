using System;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Public.ClientData.Money.Table.Main
{
    public class MainMoneyTableSummaryResponseClientData
    {
        public decimal StartBalance { get; set; }
        public decimal BankStartBalance { get; set; }
        public decimal EndBalance { get; set; }
        public decimal BankEndBalance { get; set; }
        public int IncomingCount { get; set; }
        public decimal IncomingBalance { get; set; }
        public decimal BankIncomingBalance { get; set; }
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime IncomingDate { get; set; }
        public int OutgoingCount { get; set; }
        public decimal OutgoingBalance { get; set; }
        public decimal BankOutgoingBalance { get; set; }
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime OutgoingDate { get; set; }
        public int TotalCount { get; set; }
        public bool HasOperations { get; set; }
        public bool HasBankBalance { get; set; }
        public Currency Currency { get; set; }
    }
}