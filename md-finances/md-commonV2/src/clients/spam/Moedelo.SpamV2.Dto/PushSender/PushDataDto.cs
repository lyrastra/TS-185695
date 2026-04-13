using System.Collections.Generic;

namespace Moedelo.SpamV2.Dto.PushSender
{
    public class PushDataDto
    {
        public PushDataDto()
        {
            Parameters = new Dictionary<string, object>();
        }

        public string Message { get; set; }

        public IDictionary<string, object> Parameters { get; set; }
    }
}