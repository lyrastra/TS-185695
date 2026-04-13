using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming
{
    public class IncomingCurrencySaleDto
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }
        
        public int? FromSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
        /// </summary>
        public decimal Sum { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

        public string SourceFileId { get; set; }
        public long? DuplicateId { get; set; }
        public OperationState OperationState { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}