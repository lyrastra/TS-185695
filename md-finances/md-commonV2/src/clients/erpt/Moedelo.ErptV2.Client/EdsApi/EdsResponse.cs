using System.Collections.Generic;

namespace Moedelo.ErptV2.Client.EdsApi
{
    public class EdsResponse
    {
        public bool Success { get; set; }
        public List<string> ClientWarnings { get; set; }
    }
}
