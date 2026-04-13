using System;
using System.Collections.Generic;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto.Report
{
    public class AutoBillingReportResponseDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IReadOnlyCollection<int> InitiateIds { get; set; }
        public IReadOnlyCollection<AutoBillingReportRowDto> Rows { get; set; }
        public ProductTypeEnum ProductType { get; set; }
    }
}
