using System;
using Newtonsoft.Json;
using Moedelo.InfrastructureV2.Json.Converters;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class StatementRequestBySourceClientData
    {
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? StartDate { get; set; }
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? EndDate { get; set; }
        public MoneySourceType SourceType { get; set; }
        public long SourceId { get; set; }
    }
}
