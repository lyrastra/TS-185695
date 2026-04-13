using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public class CommissionAgentAutocompleteRequestDto
    {
        public DateTime? Date { get; set; }
        public string Query { get; set; }
        public int Count { get; set; } = 10;
    }
}
