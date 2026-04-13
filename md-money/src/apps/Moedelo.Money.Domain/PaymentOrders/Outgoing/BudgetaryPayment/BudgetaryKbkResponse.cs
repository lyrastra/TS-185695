namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkResponse
    {
        public long Id { get; set; }

        public bool IsForIp { get; set; }

        public string Name { get; set; }

        public long SubcontoId { get; set; }
    }
}
