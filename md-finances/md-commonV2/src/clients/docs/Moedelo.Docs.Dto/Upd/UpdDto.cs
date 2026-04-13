using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.Docs.Dto.Upd
{
    public class UpdDto
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public AccountingDocumentType DocumentType => AccountingDocumentType.Upd;

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public bool UseNds => NdsPositionType != NdsPositionType.None;

        public NdsPositionType NdsPositionType { get; set; }

        public TaxationSystemType TaxSystem { get; set; }
        
        public bool ProvideInAccounting { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }

        public bool DebitToMainStock { get; set; }

        /// <summary>
        /// Счёт контрагента
        /// </summary>
        public SyntheticAccountCode KontragentAccountCode { get; set; }

        public long SubcontoId { get; set; }

        public UpdStatus Status { get; set; }

        public DateTime? ForgottenDocumentDate { get; set; }

        public string ForgottenDocumentNumber { get; set; }
        
        public long? StockId { get; set; }
    }
}