using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount
{
    public class RefundToSettlementAccountResponse : IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public ContractorWithRequisites Contractor { get; set; }

        public bool ProvideInAccounting { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public bool IsReadOnly { get; set; }
        // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
        //public RemoteServiceResponse<IReadOnlyCollection<BillLink>> Bills { get; set; }
        //public bool IncludeNds { get; set; }

        //public NdsType? NdsType { get; set; }

        //public decimal? NdsSum { get; set; }

        public long? DuplicateId { get; set; }

        /// <summary>
        /// Операция создана из импорта
        /// </summary>
        public bool IsFromImport { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
