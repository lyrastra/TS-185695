using Moedelo.Common.Enums.Enums.Home;

namespace Moedelo.AccountManagement.Dto.SharedFirms
{
    public class CreateFirmInAccountResultDto
    {
        public int FirmId { get; set; }
        public bool Success { get; set; }
        public RegistrationResponseStatusCode StatusCode { get; set; }
    }
}