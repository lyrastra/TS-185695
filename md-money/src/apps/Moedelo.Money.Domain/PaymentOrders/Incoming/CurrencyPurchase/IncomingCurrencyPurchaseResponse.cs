using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase
{
    public class IncomingCurrencyPurchaseResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }
        
        public int? FromSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }

        public string Description { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }
        
        public bool IsReadOnly { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}