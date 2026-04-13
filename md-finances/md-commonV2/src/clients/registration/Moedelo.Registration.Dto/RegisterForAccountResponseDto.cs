namespace Moedelo.Registration.Dto
{
    public class RegisterForAccountResponseDto
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public bool IsSuccess { get; set; }

        public RegistrationError Error { get; set; }

        public bool HasSentForReprocessing { get; set; }
    }
}