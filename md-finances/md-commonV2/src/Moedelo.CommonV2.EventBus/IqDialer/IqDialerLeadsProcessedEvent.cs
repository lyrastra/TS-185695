using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moedelo.CommonV2.EventBus.IqDialer
{
    public class IqDialerLeadsProcessedEvent
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("data")]
        public IReadOnlyCollection<IqDialerLeadsProcessedDataModel> Data { get; set; }
    }

    public class IqDialerLeadsProcessedDataModel
    {
        [JsonProperty("lead")]
        public IqDialerLeadsProcessedLeadModel Lead { get; set; }

        [JsonProperty("call")]
        public IqDialerLeadsProcessedCallModel Call { get; set; }
    }

    public class IqDialerLeadsProcessedLeadModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("external_id")]
        public Guid ExternalId { get; set; }
    }

    public class IqDialerLeadsProcessedCallModel
    {
        [JsonProperty("disposition")]
        public string Status { get; set; }
    }
}