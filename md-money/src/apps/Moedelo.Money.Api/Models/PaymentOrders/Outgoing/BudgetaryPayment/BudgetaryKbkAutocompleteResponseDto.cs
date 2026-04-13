namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkAutocompleteResponseDto
    {
        public long Id { get; internal set; }

        public bool IsForIp { get; internal set; }

        public string Name { get; internal set; }

        public long SubcontoId { get; internal set; }
    }
}
