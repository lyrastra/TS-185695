using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Handler.Dto
{
    public class BillToPaymentOrderModel
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Реквизиты получателя, Моё Дело
        /// </summary>
        public int RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientInn { get; set; }
        public string RecipientKpp { get; set; }
        public string RecipientSettlementAccount { get; set; }
        public string RecipientBankName { get; set; }
        public string RecipientBankBik { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        public OperationType OperationType => OperationType.PaymentOrderOutgoingPaymentToSupplier;
    }
}