using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkDefaultsDto
    {
        /// <summary>
        /// статус платильщика (101)
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        /// <summary>
        /// 106 "Основание платежа"
        /// </summary>
        public BudgetaryPaymentBase PaymentBase { get; set; }

        /// <summary>
        /// 110 "Тип платежа"
        /// </summary>
        public BudgetaryPaymentType PaymentType { get; set; }

        /// <summary>
        /// Дата документа-требования (109)
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// Номер документа-требования (108)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        ///  Получатель
        /// </summary>
        public BudgetaryRecipientResponseDto Recipient { get; set; }

        /// <summary>
        /// Назначение платежа (24)
        /// </summary>
        public string Description { get; set; }
    }
}
