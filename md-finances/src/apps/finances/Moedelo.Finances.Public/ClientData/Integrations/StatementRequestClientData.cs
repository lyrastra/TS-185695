using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;
using System;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class StatementRequestClientData
    {
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? StartDate { get; set; }
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? EndDate { get; set; }
    }
}
