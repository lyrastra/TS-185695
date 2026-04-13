using System.Collections.Generic;
using Newtonsoft.Json;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptSearchResponseDto 
    {
        [JsonProperty("rows")]
        public List<SignedExcerptSearchRowDto> Rows { get; set; }
    }
}