using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.AccountingPostings
{
    public class AccountingPostingDescriptionClientData
    {
        public long Id { get; set; }

        [JsonConverter(typeof(MdDateConverter))]
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int Debit { get; set; }

        public int Credit { get; set; }

        public long DebitTypeId { get; set; }

        public long CreditTypeId { get; set; }

        public string DebitNumber { get; set; }

        public string CreditNumber { get; set; }

        public string Description { get; set; }

        public List<SubcontoClientData> SubcontoDebit { get; set; }= new List<SubcontoClientData>();

        public List<SubcontoClientData> SubcontoCredit { get; set; } = new List<SubcontoClientData>();

        public SyntheticAccountBalanceType CreditBalanceType { get; set; }

        public SyntheticAccountBalanceType DebitBalanceType { get; set; }

        public bool IsValid
        {
            get { return Credit > 0 && Debit > 0; }
        }
    }
}