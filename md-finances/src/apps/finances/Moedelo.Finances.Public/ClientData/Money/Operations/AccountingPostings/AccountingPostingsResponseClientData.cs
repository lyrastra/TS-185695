using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.AccountingPostings
{
    public class AccountingPostingsResponseClientData
    {
        public AccountingPostingsResponseClientData()
        {
            Operations = new List<AccountingPostingsClientData>();
            Status = true;
        }

        public AccountingPostingsResponseClientData(string message) : this()
        {
            Message = message;
        }

        public List<AccountingPostingsClientData> Operations { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }
    }
}