namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class SubKbkAutocompleteResponseDto
    {
        public long Id { get; internal set; }

        public bool IsForIp { get; internal set; }

        public string Name { get; internal set; }

        public long SubcontoId { get; internal set; }
    }
}
