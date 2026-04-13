using System;

namespace Moedelo.TaxRemains.Client.Dto
{
    public class FixedPaymentRemainsDto
    {
        public DateTime BalanceDate { get; set; }

        public decimal PensionInsuranceQuarter1 { get; set; }

        public decimal PensionInsuranceQuarter2 { get; set; }

        public decimal PensionInsuranceQuarter3 { get; set; }

        public decimal MedicalInsuranceQuarter1 { get; set; }

        public decimal MedicalInsuranceQuarter2 { get; set; }

        public decimal MedicalInsuranceQuarter3 { get; set; }

        public decimal AdditionalPaymentQuarter1 { get; set; }

        public decimal AdditionalPaymentQuarter2 { get; set; }

        public decimal AdditionalPaymentQuarter3 { get; set; }

        public decimal PensionInsuranceCurrentYear { get; set; }

        public decimal MedicalInsuranceCurrentYear { get; set; }

        public decimal AdditionalPaymentCurrentYear { get; set; }

        public decimal AdditionalPaymentPreviousYear { get; set; }

        public decimal UsnIncomeCurrentYear { get; set; }

        public decimal UsnIncomePreviousYear { get; set; }

        public decimal EnvdIncomeCurrentYear { get; set; }

        public decimal EnvdIncomePreviousYear { get; set; }

        public decimal? OsnoIncomeCurrentYear { get; set; }
        public decimal? OsnoIncomePreviousYear { get; set; }
    }
}
