using System;

namespace Moedelo.PayrollV2.Client.SalarySettings.DTO
{
    public class SalarySettingDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int DaySalaryPayment { get; set; }
        
        public int DayAdvancePayment { get; set; }

        public int SalaryPaymentPeriod { get; set; }

        public int PaymentRounding { get; set; }

        public int AdvanceCalculationType { get; set; }

        public int TerritorialCondition { get; set; }

        public int? SalarySettlementAccountId { get; set; }

        public DateTime? SalaryChargingStartDate { get; set; }

        public bool IsPilotProject => PilotProjectStartDate.HasValue;

        public DateTime? PilotProjectStartDate { get; set; }

        public bool? IsCalculateTaxReportByCharges { get; set; }

        public int? SalaryProjectSettlementAccountId { get; set; }

        public string SalaryProjectAgreementNumber { get; set; }

        public bool IsAutoAdvancePayment { get; set; }

        public bool IsAutoSalaryPayment { get; set; }

        public bool IsAutoWorkContractPayment { get; set; }

        public bool IsAutoPaymentEditable { get; set; }
    }
}
