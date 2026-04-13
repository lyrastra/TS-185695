using System;
using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class OpportunityCallCenterSendingCommand
    {
        public int FirmId { get; set; }

        public int? RegistrationHistoryId { get; set; }

        public RegistrationStatus? RegistrationStatus { get; set; }

        public string RegistrationProduct { get; set; }

        public string SuiteOpportunityId { get; set; }

        public string SuiteCrmLeadId { get; set; }

        public string Bucket { get; set; }

        public string BucketRuleName { get; set; }

        /// <summary>
        /// Проверять наличие оплаты от клиента с лендинга перед обработкой
        /// </summary>
        public bool CheckLeadPaymentFromLandingPageBeforeProcessing { get; set; }

        public string StrategyCode { get; set; }

        public int LeadDistributionPercentageInVoxys { get; set; }
    }
}