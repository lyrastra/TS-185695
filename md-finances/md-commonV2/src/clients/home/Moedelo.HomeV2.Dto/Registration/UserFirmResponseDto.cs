using Moedelo.Common.Enums.Enums.Home;

namespace Moedelo.HomeV2.Dto.Registration
{
    public class UserFirmResponseDto
    {
        public int UserId { get; set; }
        
        public RegistrationResponseStatusCode StatusCode { get; set; }
    }
}