namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee
{
    /// <summary>
    /// Меняет сотрудника в п/п без указанного сотрудника
    /// </summary>
    public class PaymentOrderSetMissingEmployeeCommand
    {
        public long[] DocumentBaseIds { get; set; }

        public int EmployeeId { get; set; }
    }
}