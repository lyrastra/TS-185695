using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash
{
    public class TransferFromCashSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Назначение платежа (описание)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Проводится ли операция в бухгалтерском учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// DocumentBaseId расходно-кассового ордера
        /// </summary>
        public long? CashOrderBaseId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public OutsourceState? OutsourceState { get; set; }

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