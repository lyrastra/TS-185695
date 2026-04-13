using Newtonsoft.Json;
using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.IqDialer
{
    public class IqDialerCallEndedEvent
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("data")]
        public IReadOnlyCollection<IqDialerCallEndedDataModel> Data { get; set; }
    }

    public class IqDialerCallEndedDataModel
    {
        [JsonProperty("campaign")]
        public IqDialerCallEndedCampaignModel Campaign { get; set; }

        [JsonProperty("lead")]
        public IqDialerCallEndedLeadModel Lead { get; set; }

        [JsonProperty("call")]
        public IqDialerCallEndedCallModel Call { get; set; }
    }

    public class IqDialerCallEndedCampaignModel
    {
        [JsonProperty("strategy")]
        public string Strategy { get; set; }
    }

    public class IqDialerCallEndedLeadModel
    {
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }
    }

    public class IqDialerCallEndedCallModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("agent_ext")]
        public int? AgentExternalId { get; set; }

        [JsonProperty("time_talk_call")]
        public int? TimeTalkCall { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }
    }
}