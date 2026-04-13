using System;

namespace Moedelo.Docs.Dto.Upd
{
    public class UpdAsReasonForPaymentResponseDto
    {
        public long DocumentId { get; set; }

        public long DocumentBaseId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentName { get; set; }

        public decimal UnpaidBalance { get; set; }

        public decimal Sum { get; set; }
    }
}