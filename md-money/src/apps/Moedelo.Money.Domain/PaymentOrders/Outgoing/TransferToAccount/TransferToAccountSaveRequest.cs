using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount
{
    public class TransferToAccountSaveRequest : IActualizableSaveRequest, IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        public int? ToSettlementAccountId { get; set; }

        public long TransferFromAccountBaseId { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }

        public OperationState OperationState { get; set; }

        public long? DuplicateId { get; set; }

        public string SourceFileId { get; set; }

        public bool IsIgnoreNumber { get; set; }
        public OutsourceState? OutsourceState { get; set; }

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
