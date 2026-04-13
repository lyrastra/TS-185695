using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class BankStatementResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> MessageList { get; set; }
        public string PhoneMask { get; set; }
    }
}