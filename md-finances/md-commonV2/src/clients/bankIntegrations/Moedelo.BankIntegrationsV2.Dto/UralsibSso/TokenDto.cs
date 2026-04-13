using System;

namespace Moedelo.BankIntegrationsV2.Dto.UralsibSso
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime SessionLastDate { get; set; }
        public string RefreshToken { get; set; }
        public bool NeedUpdateInDb { get; set; }
    }
}
