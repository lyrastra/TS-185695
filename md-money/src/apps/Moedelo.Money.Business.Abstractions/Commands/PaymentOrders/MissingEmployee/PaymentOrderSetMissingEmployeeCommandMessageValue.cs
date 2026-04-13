using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee
{
    public sealed class PaymentOrderSetMissingEmployeeCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public long[] DocumentBaseIds { get; set; }

        public int EmployeeId { get; set; }
    }
}
