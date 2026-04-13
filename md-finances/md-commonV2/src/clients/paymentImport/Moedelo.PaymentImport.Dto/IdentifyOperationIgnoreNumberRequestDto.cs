using Moedelo.Common.Enums.Enums.PostingEngine;
using System;

namespace Moedelo.PaymentImport.Dto
{
    public class IdentifyOperationIgnoreNumberRequestDto
    {
        public Guid Guid { get; set; }

        public DateTime Date { get; set; }

        public string PaymentPurpose { get; set; }

        public OperationType? OperationType { get; set; }
    }
}
