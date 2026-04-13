using System;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.WebApp.ClientData.Integrations
{
    public class StatementRequestByIntegrationPartnerClientData
    {
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? StartDate { get; set; }
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? EndDate { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}