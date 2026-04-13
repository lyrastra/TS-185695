using System;

namespace Moedelo.TaxRemains.Client.Dto
{
    public class TaxRemainDto
    {
        public int? FirmId { get; set; }
        public DateTime BalanceDate { get; set; }
        public decimal ProfitQuarter1 { get; set; } = 0;
        public decimal ProfitQuarter2 { get; set; } = 0;
        public decimal ProfitQuarter3 { get; set; } = 0;
        public decimal ExpenseQuarter1 { get; set; } = 0;
        public decimal ExpenseQuarter2 { get; set; } = 0;
        public decimal ExpenseQuarter3 { get; set; } = 0;
        public decimal AdvancePaymentQuarter1 { get; set; } = 0;
        public decimal AdvancePaymentQuarter2 { get; set; } = 0;
        
        public decimal PensionInsuranceQuarter1 { get; set; }
        public decimal PensionInsuranceQuarter2 { get; set; }
        public decimal PensionInsuranceQuarter3 { get; set; }
        public decimal FssDisabledQuarter1 { get; set; }
        public decimal FssDisabledQuarter2 { get; set; }
        public decimal FssDisabledQuarter3 { get; set; }
        public decimal MedicalInsuranceQuarter1 { get; set; }
        public decimal MedicalInsuranceQuarter2 { get; set; }
        public decimal MedicalInsuranceQuarter3 { get; set; }
        public decimal FssTraumatismQuarter1 { get; set; }
        public decimal FssTraumatismQuarter2 { get; set; }
        public decimal FssTraumatismQuarter3 { get; set; }
        public decimal SicklistQuarter1 { get; set; }
        public decimal SicklistQuarter2 { get; set; }
        public decimal SicklistQuarter3 { get; set; }
        public decimal TradingTaxQuarter1 { get; set; }
        public decimal TradingTaxQuarter2 { get; set; }
        public decimal TradingTaxQuarter3 { get; set; }
        public decimal PaymentFederalBudget { get; set; }
        public decimal PaymentRegionBudget { get; set; }

        /// <summary>
        /// Авансовые платежи
        /// </summary>
        public TaxRemainIpOsnoAdvancePaymentDto[] AdvancePayments { get; set; } 
    }
}
