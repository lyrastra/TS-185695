using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Autocomplete
{
    public class PaymentAutocompleteResult
    {
        public int TotalCount { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public List<PaymentAutocompleteItem> List { get; set; }
    }
}
