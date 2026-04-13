using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash
{
    public class TransferFromCashImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Назначение платежа (описание)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// DocumentBaseId расходно-кассового ордера
        /// </summary>
        public long? CashOrderBaseId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}