using Moedelo.Money.Domain.LinkedDocuments;
using System;
using System.Collections.Generic;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public class PaymentToAccountablePersonResponse : IActualizableReadResponse, IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Employee Employee { get; set; }

        public bool ProvideInAccounting { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<AdvanceStatementLink>> AdvanceStatements { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Признак: проведено вручную в налоговом учете
        /// </summary>
        public bool TaxPostingsInManualMode { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsFromImport { get; set; }
    }
}
