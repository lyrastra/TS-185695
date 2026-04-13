namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee
{
    public sealed class PaymentOrderSetMissingEmployeeCommandMessage
    {
        public long[] DocumentBaseIds { get; set; }

        public int EmployeeId { get; set; }
    }
}
