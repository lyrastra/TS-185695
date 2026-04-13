namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    public class BudgetaryKbkAutocompleteResponseDto
    {
        public long Id { get; set; }

        public bool IsForIp { get; set; }

        public string Name { get; set; }

        public long SubcontoId { get; set; }
    }
}
