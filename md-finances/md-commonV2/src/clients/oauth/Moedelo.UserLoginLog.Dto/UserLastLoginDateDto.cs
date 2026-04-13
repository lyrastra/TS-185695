using System;

namespace Moedelo.UserLoginLog.Dto
{
    public class UserLastLoginDateDto
    {
        public int UserId { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}