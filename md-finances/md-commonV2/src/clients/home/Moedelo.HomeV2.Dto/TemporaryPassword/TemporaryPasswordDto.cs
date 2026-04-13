using System;

namespace Moedelo.HomeV2.Dto.TemporaryPassword
{
    public class TemporaryPasswordDto
    {
        public int UserId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Value { get; set; }
    }
}