using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.Registration.Dto
{
    public class RegistrationResultDto
    {
        public bool Success { get; set; }

        public int? RegistrationHistoryId { get; set; }

        public RegistrationStatus? RegistrationStatus { get; set; }

        public RegistrationError Error { get; set; }

        public string PartnerClientUid { get; set; }

        public RegistrationKeyInfoDto Info { get; set; }

        /// <summary>
        /// Разрешена ли оплата на лендинге
        /// </summary>
        public bool IsPaymentAllowedOnLandingPage { get; set; }

        public bool HasSentForReprocessing { get; set; }
    }
}