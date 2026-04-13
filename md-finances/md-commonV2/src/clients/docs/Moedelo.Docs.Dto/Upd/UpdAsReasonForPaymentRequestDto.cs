using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.Upd
{
    public class UpdAsReasonForPaymentRequestDto
    {
        public string Query { get; set; }

        public int? KontragentId { get; set; }

        public int? KontragentAccountCode { get; set; }

        public long? BillBaseId { get; set; }

        public long? ContractBaseId { get; set; }

        public int Count { get; set; }
        
        public PrimaryDocumentsTransferDirection Direction { get; set; }

        public long? PaymentBaseId { get; set; }
    }
}