using Newtonsoft.Json;
using System;

namespace Moedelo.Chat.Reports.Dto
{
    public class BaseReportRequestDto
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }
    }
}
