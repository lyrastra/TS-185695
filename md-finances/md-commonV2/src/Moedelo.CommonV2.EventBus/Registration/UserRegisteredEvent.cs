using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.CommonV2.EventBus.Registration
{
    public class UserRegisteredEvent
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public bool IsInternal { get; set; }

        public bool IsReactivated { get; set; }

        public bool IsLeadMarkChanged { get; set; }

        public int ReferralId { get; set; }

        public RegistrationStatus RegistrationStatus { get; set; }

        public RegistrationMethod RegistrationMethod { get; set; }

        public bool IsTargetLead { get; set; }

        public int RegistrationHistoryId { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// Email, который был указан при регистрации
        /// </summary>
        public string CurrentRegistrationEmail { get; set; }
    }
}