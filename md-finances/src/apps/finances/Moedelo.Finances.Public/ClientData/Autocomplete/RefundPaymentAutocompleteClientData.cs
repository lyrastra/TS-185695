using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Autocomplete
{
    public class RefundPaymentAutocompleteClientData
    {
        public long TotalCount { get; set; }

        public long Offset { get; set; }

        public long Limit { get; set; }

        public List<RefundPaymentAutocompleteItemClientData> Data { get; set; }
    }
}