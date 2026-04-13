namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.RabbitMQ
{
    public class MovementListEvent
    {
        public int FirmId { get; set; }

        public bool IsUserMovement { get; set; }

        public string MongoObjectId { get; set; }
    }
}
