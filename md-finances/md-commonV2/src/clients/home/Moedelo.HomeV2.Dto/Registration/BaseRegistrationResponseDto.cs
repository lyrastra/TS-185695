using Moedelo.Common.Enums.Enums.Home;

namespace Moedelo.HomeV2.Dto.Registration
{
    public class BaseRegistrationResponseDto
    {
        public RegistrationResponseStatusCode StatusCode { get; set; }
        
        public int FirmId { get; set; }

        public int UserId { get; set; }
    }
}