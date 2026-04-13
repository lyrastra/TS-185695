using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentResponse : IActualizableReadResponse, IAccessorPropsResponse
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
        /// Признак: нужно ли проводить в бухгалтерском учете
        /// </summary>

        public BudgetaryAccountCodes AccountCode { get; set; }

        public int KbkId { get; set; }

        public string KbkNumber { get; set; }

        public UnifiedBudgetaryRecipient Recipient { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// УИН
        /// </summary>
        public string Uin { get; set; }

        public bool IsReadOnly { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Статус плательщика
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPayment> SubPayments { get; set; }
    }
}
