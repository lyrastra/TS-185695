using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public class CurrencyTransferFromAccountResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }
        
        public int? FromSettlementAccountId { get; set; }

        /// <summary>
        /// Назначение платежа (описание)
        /// </summary>
        public string Description { get; set; }

        public bool IsReadOnly { get; set; }

        public long? DuplicateId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
    }
}