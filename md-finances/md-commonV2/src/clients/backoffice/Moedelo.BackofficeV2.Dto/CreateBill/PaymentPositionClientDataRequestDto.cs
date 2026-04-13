using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BackofficeV2.Dto.CreateBill
{
    public class PaymentPositionClientDataRequestDto
    {
        public PaymentPositionType Type { get; set; }

        public OneTimeServiceType? OneTimeType { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public bool HasNds { get; set; }
        
        public decimal RegionalRatio { get; set; }
    }
}
