using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment
{
    public class RentPaymentSaveRequest : IActualizableSaveRequest, IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public long? ContractBaseId { get; set; }

        public long? InventoryCardBaseId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public bool ProvideInAccounting { get; set; }

        public IReadOnlyCollection<RentPeriod> RentPeriods { get; set; }

        public bool IsPaid { get; set; }

        public OperationState OperationState { get; set; }

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
