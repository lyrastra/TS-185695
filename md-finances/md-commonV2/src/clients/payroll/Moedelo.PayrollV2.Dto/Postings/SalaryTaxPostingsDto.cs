using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Payroll;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class SalaryTaxPostingsDto
    {
        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        public string WorkerName { get; set; }

        public int WorkerId { get; set; }

        public decimal Sum { get; set; }

        public string Date { get; set; }

        public string DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public AccountingDocumentType DocumentType { get; set; }

        public string Description { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        /// <summary>
        /// Тип проводки (зарплатная, НДФЛ, Фонды)
        /// </summary>
        public TaxPostingType TaxPostingType { get; set; }

        /// <summary>
        /// Счет фонда
        /// </summary>
        public SyntheticAccountCode FundAccountCode { get; set; }
    }
}