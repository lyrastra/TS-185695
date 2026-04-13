using System;

namespace Moedelo.CommonV2.EventBus.Crm
{
    public class TaskDialerSendingWithVerifyCommand
    {
        public int FirmId { get; set; }

        public int? RegistrationHistoryId { get; set; }

        /// <summary>
        /// Проверять наличие оплаты от клиента с лендинга перед обработкой
        /// </summary>
        public bool CheckLeadPaymentFromLandingPageBeforeProcessing { get; set; }

        public string TaskId { get; set; }

        public string TaskSubject { get; set; }

        public string TaskPhone { get; set; }

        public string TaskCrmUrl { get; set; }

        public string OpportunityFullClientName { get; set; }

        public string OpportunityTimeZone { get; set; }

        public int? LeadQualityGroup { get; set; }

        public int? LeadTimeZone { get; set; }

        public string BucketId { get; set; }

        public DateTime? NextTimeCall { get; set; }
    }
}