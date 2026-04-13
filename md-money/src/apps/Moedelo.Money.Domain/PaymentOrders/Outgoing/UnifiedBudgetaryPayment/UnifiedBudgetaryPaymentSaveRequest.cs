using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentSaveRequest : IActualizableSaveRequest, IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа (7)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Назначение платежа (24)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Реквизиты получателя
        /// </summary>
        public UnifiedBudgetaryRecipient Recipient { get; set; }

        /// <summary>
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>
        public bool? ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// УИН
        /// </summary>
        public string Uin { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveModel> SubPayments { get; set; }

        public string SourceFileId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Статус плательщика
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        public bool IsSaveNumeration { get; set; }

        /// <summary>
        /// Идентификаторы применённых правил импорта
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Идентификатор лога импорта
        /// </summary>
        public int? ImportLogId { get; set; }
    }
}
