using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.ChatPlatform
{
    public class SendClientDispatchCommand
    {
        public int DispathcId { get; set; }

        public string ClientLogin { get; set; }

        public List<int> DispatchItems { get; set; }
    }
}
