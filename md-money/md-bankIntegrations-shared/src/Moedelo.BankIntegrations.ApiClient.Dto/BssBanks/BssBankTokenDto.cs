using System;

namespace Moedelo.BankIntegrations.Dto.BssBanks
{
    public class BssBankTokenDto
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public int SessionExpirationTime { get; set; }
        public string[] TokenScope { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
