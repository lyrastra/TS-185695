using System.Collections.Generic;

namespace Moedelo.Finances.WebApp.ClientData.Integrations
{
    public class BankStatementResponseClientData
    {
        public string PhoneMask { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> MessageList { get; set; }
    }
}