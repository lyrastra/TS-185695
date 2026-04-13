using System;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Finances.Domain.Enums.Money;
using Newtonsoft.Json;
using Moedelo.InfrastructureV2.Json.Converters;
using Moedelo.Finances.Public.ClientData.Money.Table.Main;
using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money
{
    public class MoneyOperationClientData
    {
        public long DocumentBaseId { get; set; }
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public MoneyDirection Direction { get; set; }
        public string KontragentName { get; set; }
        public OperationType OperationType { get; set; }
        public DocumentStatus PaidStatus { get; set; }
        public decimal Sum { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public IReadOnlyCollection<AppliedImportRuleClientData> ImportRules { get; set; }
        public IReadOnlyCollection<AppliedImportRuleClientData> OutsourceImportRules { get; set; }
    }
}