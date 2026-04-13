using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.TaxPostings
{
    public class TaxPostingsResponseClientData
    {
        public List<TaxPostingsClientData> Operations { get; set; }

        public ProvidePostingType PostingsAndTaxMode { get; set; }

        public string ExplainingMessage { get; set; }
    }
}