using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Upd
{
    public class UpdAsPossibleInvoiceReasonResponseDto
    {
        public int Id { get; set; }
        public long DocumentBaseId { get; set; }
        public string DocumentNumber { get; set; }
        public decimal DocumentSum { get; set; }
        public DateTime DocumentDate { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
        public int KontragentId { get; set; }
        public NdsPositionType? NdsPositionType { get; set; }
    }
}