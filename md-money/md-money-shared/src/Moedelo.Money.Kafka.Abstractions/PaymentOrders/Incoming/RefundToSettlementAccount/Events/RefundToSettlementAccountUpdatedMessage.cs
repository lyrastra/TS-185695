using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Events
{
    public class RefundToSettlementAccountUpdatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = new Contractor();

        public long? ContractBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Выбранная СНО в операции
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты

        //public IReadOnlyCollection<BillLink> BillLinks { get; set; } = Array.Empty<BillLink>();

        ///// <summary>
        ///// В том числе НДС
        ///// </summary>
        //public Nds Nds { get; set; }
    }
}
