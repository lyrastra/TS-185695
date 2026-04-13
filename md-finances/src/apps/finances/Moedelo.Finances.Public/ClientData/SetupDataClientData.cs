using System;
using System.Collections.Generic;
using Moedelo.Finances.Public.ClientData.Integrations;
using Moedelo.Finances.Public.ClientData.Setup;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Public.ClientData
{
    public class SetupDataClientData
    {
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? RegistrationDate { get; set; }

        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? BalanceDate { get; set; }

        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? LastClosedDate { get; set; }

        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? RegistrationInService { get; set; }

        public AccessRuleFlagsClientData AccessRuleFlags { get; set; }

        public string ImportMessages { get; set; }

        public List<IntegrationErrorClientData> IntegrationErrors { get; set; }
    }
}
