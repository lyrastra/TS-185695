using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BillingV2.Dto.Billing.PaymentPositions
{
    public class PaymentPositionDto
    {
        public PaymentPositionType Type { get; set; }

        public OneTimeServiceType? OneTimeType { get; set; }

        public decimal Price { get; set; }

        public decimal MinPrice { get; set; }

        public string Name { get; set; }

        public bool HasNds { get; set; }

        /// <summary>
        /// не импортировать в 1С 
        /// </summary>
        public bool IsExcludedFrom1C { get; set; }

        public decimal RegionalRatio { get; set; } = 1;

        public string ProductCode { get; set; }
        
        public string ProductConfigurationCode { get; set; }

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;

        public string NameForEmail { get; set; }

        public decimal? NormativePrice { get; set; }

        public IReadOnlyCollection<PaymentPositionSellerDto> Sellers { get; set; }
    }
}
