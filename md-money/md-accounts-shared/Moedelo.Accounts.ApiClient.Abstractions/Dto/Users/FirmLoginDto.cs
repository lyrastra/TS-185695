using System;

namespace Moedelo.Accounts.Abstractions.Dto.Users
{
    public sealed class FirmLoginDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
    }
}
