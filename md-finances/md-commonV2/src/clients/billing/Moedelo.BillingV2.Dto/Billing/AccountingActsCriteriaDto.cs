using System;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class AccountingActsCriteriaDto
    {
        public int[] OnlyFirmIds { get; set; }
        public bool? IsFirmInternal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string[] ExcludePaymentMethods { get; set; }
        public string[] OnlyProductGroups { get; set; }
    }
}