using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentDto
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
        /// Идентификатор КБК основного платежа
        /// </summary>
        public int KbkId { get; set; }

        public UnifiedBudgetaryRecipientRequisitesDto Recipient { get; set; }

        /// <summary>
        /// Признак: Оплачен
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// УИН
        /// </summary>
        public string Uin { get; set; }

        public OperationState OperationState { get; set; }
        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Режим виджета БУ/НУ "Учитывать вручную"
        /// </summary>
        public ProvidePostingType PostingsAndTaxMode { get; set; } = ProvidePostingType.Auto;

        /// <summary>
        /// Режим виджета НУ "Учитывать вручную"
        /// </summary>
        public ProvidePostingType TaxPostingType { get; set; } = ProvidePostingType.Auto;

        /// <summary>
        /// Учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; } = true;
        
        /// <summary>
        /// Статус плательщика
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> SubPayments { get; set; }
    }
}
