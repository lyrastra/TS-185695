using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalarySettings
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
    }
}
