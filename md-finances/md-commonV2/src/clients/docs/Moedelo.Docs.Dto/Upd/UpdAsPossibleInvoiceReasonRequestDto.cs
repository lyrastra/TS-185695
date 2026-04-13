using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Upd
{
    public class UpdAsPossibleInvoiceReasonRequestDto
    {
        public PrimaryDocumentsTransferDirection Direction { get; set; }
        public string Query { get; set; }
        public int? KontragentId { get; set; }
        public int Count { get; set; }
        public DateTime? SinceDate { get; set; }
    }
}