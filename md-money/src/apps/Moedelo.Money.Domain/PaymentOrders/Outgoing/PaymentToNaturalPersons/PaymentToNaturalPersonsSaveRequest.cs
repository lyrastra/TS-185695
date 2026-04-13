using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class PaymentToNaturalPersonsSaveRequest : IActualizableSaveRequest, IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }
        
        public bool ProvideInAccounting { get; set; }

        public long? DuplicateId { get; set; }
        
        public string SourceFileId { get; set; }

        public bool IsPaid { get; set; }

        public IReadOnlyCollection<EmployeePaymentsSaveModel> EmployeePayments { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsSaveNumeration { get; set; }
        
        public bool? IsPaidStatusChanged { get; set; }

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
