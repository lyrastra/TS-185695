using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.Money.Kafka.PaymentOrders
{
    public sealed class CUDCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public CUDCommandType CommandType { get; set; }

        public string CommandDataJson { get; set; }
    }

    public enum CUDCommandType
    {
        Create = 1,
        Update = 2,
        Delete = 3,
    }
}
