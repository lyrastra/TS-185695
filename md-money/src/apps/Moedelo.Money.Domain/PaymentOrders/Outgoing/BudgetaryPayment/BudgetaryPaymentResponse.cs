using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.Operations;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentResponse : IActualizableReadResponse, IAccessorPropsResponse
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        /// <summary>
        /// Для ui не используется, возможно понадобится для генерации бланков п/п
        /// </summary>
        public BudgetaryPaymentType PaymentType { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public BudgetaryPayerStatus PayerStatus { get; set; }

        public BudgetaryPaymentBase PaymentBase { get; set; }

        public string DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public string Uin { get; set; }

        public BudgetaryRecipient Recipient { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReadOnly { get; set; }

        public int? TradingObjectId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
        
        public long? PatentId { get; set; }
        
        public RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> CurrencyInvoices { get; set; }

        public bool IsFromImport { get; set; }
    }
}
