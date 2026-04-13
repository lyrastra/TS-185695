namespace Moedelo.BackofficeV2.Dto.Partner
{
    public class PartnerNameAndSelfMaintainedResponseDto
    {
        public string PartnerName { get; set; }

        public bool? IsSelfMaintained { get; set; }

        public bool IsWorkingWithCrm { get; set; }

        public bool ResponseStatus { get; set; }

        public string ResponseMessage { get; set; }
    }
}