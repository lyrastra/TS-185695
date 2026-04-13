using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class SavedWorkerPaymentDto
    {
        public int WorkerId { get; set; }

        public decimal Sum { get; set; }

        public string Date { get; set; }

        public WorkerPaymentType PaymentType { get; set; }

        public string DocumentNumber { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        //public MoneyBayType MoneyBayType { get; set; }
        
        public string DocumentTypeDescription { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public DateTime PaymentDate
        {
            get
            {
                DateTime date;
                DateTime.TryParse(Date, out date);
                return date;
            }
        }

        public long? DocumentBaseId { get; set; }
    }
}