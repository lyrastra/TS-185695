using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Events
{
    public class CurrencyBankFeeDeletedMessage
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор нового платежа, созданного вместо удаляемого.
        /// Заполняется при смене типа операции.
        /// </summary>
        public long? NewDocumentBaseId { get; set; }
    }
}