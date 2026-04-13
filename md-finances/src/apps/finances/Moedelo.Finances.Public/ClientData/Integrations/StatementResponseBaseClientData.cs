using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class StatementResponseBaseClientData
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> MessageList { get; set; }
    }
}