using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.AccountingV2.Dto.Requsites
{
    public class FirmRequisitesV2Dto
    {
        public bool IsOoo { get; set; }

        public bool HasEmployees { get; set; }

        public bool HasTaxationSystem { get; set; }

        public bool HasFssDisabilityDetails { get; set; }

        public bool HasPfr { get; set; }

        public string Pfr { get; set; }

        public bool HasFssInjuryDetails { get; set; }

        public bool SalaryIntegrationOn { get; set; }

        public bool IsManualCashMode { get; set; }

        public bool IsPro { get; set; }

        public TariffMode TariffMode { get; set; }

        public PayStatus CurrentPay { get; set; }

        public bool IsTrialCard { get; set; }

        public string Pseudonym { get; set; }

        public string FinancialResultLastClosedPeriod { get; set; }

        public string CashClosedDate { get; set; }

        /// <summary>
        /// Дата гос. регистрации фирмы
        /// </summary>
        public string RegistrationDate { get; set; }

        /// <summary>
        /// Дата регистрации в сервисе (UTC-формат)
        /// </summary>
        public string RegistrationInServiceDate { get; set; }

        public string BalanceDate { get; set; }

        public bool StockIsEnabled { get; set; }

        public string StockActivationDate { get; set; }

        public bool IsAccounting { get; set; }

        public bool IsMdStaff { get; set; }

        public bool IsFocusGroupFirm { get; set; }
    }
}