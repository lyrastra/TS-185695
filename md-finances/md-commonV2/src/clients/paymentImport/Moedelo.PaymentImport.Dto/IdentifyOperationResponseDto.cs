using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.PaymentImport.Dto
{
    public class IdentifyOperationResponseDto
    {
        public Guid Guid { get; set; }

        public int? ImportRuleId { get; set; }

        public OperationType? OperationType { get; set; }

        public int? TaxationSystem { get; set; }
    }
}
