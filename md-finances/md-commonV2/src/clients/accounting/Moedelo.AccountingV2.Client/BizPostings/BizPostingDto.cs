using Moedelo.Common.Enums.Enums.BalanceBiz;

namespace Moedelo.AccountingV2.Client.BizPostings
{

    public class BizPostingDto
    {
        public string Date { get; set; }

        public decimal Sum { get; set; }

        public string Debit { get; set; }

        public long? DebitSubcontoId { get; set; }

        public string Credit { get; set; }

        public long? CreditSubcontoId { get; set; }

        public int? FinancialOperationId { get; set; }

        public long? DocumentId { get; set; }

        public BizPostingDocumentType? DocumentTypeId { get; set; }
    }
}
