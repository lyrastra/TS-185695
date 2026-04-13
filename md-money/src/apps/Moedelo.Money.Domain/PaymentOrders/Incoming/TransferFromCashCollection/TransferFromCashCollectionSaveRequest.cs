using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection
{
    public class TransferFromCashCollectionSaveRequest : IPaymentOrderOutsourceSaveRequest
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

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}