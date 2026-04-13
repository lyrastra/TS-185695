using System;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Public.ClientData.Money.Operations
{
    public class LinkedDocumentClientData
    {
        public long Id { get; set; }

        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public AccountingDocumentType Type { get; set; }
    }
}