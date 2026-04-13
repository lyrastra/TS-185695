using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy
{
    public class MeetingMessageDto : BaseMessageDto
    {
        public DateTime? LearnDate { get; set; }

        public string LearnPlace { get; set; }

        public DateTime? MeetingDate { get; set; }

        public string MeetingNote { get; set; }

        public string MeetingPlace { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string RegistrationPlace { get; set; }
    }
}