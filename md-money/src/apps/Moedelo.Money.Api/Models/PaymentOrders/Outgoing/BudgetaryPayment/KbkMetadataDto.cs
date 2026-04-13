namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class KbkMetadataDto
    {
        public long Id { get; set; }
        public bool IsForIp { get; set; }
        public string Name { get; set; }
        public long SubcontoId { get; set; }
    }
}
