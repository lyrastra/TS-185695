namespace Moedelo.ReceiptStatement.Kafka.Abstractions.Messages
{
    public class ReceiptStatementDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public long SubcontoId { get; set; }
    }
}
