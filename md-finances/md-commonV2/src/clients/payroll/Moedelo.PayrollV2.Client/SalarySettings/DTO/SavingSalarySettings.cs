using System.Collections.Generic;

namespace Moedelo.PayrollV2.Client.SalarySettings.DTO
{
    public class SavingSalarySettings
    {
        public SavingSalarySettings()
        {
            FactorRows = new List<FirmFactorDto>();
            DeleteFactorRow = new List<long>();
        }

        public int SalaryDate { get; set; }

        public int AdvanceDate { get; set; }

        public string SalaryChargingStartDate { get; set; }

        public int? SalarySettlementAccountId { get; set; }

        public int Rounding { get; set; }

        public bool UseFirmFactor { get; set; }

        public int AdvanceType { get; set; }

        public string SalaryPaymentPeriod { get; set; }

        public int TerritorialCondition { get; set; }

        public List<FirmFactorDto> FactorRows { get; set; }

        public List<long> DeleteFactorRow { get; set; }

        public string PilotProjectStartDate { get; set; }

        public bool? IsCalculateTaxReportByCharges { get; set; }

        public int? SalaryProjectSettlementAccountId { get; set; }

        public string SalaryProjectAgreementNumber { get; set; }

        public bool IsAutoAdvancePayment { get; set; }

        public bool IsAutoSalaryPayment { get; set; }

        public bool IsAutoWorkContractPayment { get; set; }
    }
}
