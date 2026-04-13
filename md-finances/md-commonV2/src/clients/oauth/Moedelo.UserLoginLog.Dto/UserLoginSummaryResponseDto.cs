using System;

namespace Moedelo.UserLoginLog.Dto
{
    public class UserLoginSummaryResponseDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastPlatform { get; set; }
    }
}
