using System;

namespace Moedelo.AccountV2.Dto.User
{
    public class PasswordRecoveryRequestDto
    {
        public string Email { get; set; }

        public bool IsConfirmation { get; set; }

        public bool IsConfirmationByCode { get; set; }

        public string ConfirmationGuid { get; set; }

        public string PasswordMd5 { get; set; }

        public string ConfirmPasswordMd5 { get; set; }

        public string Host { get; set; }

        public string RedirectUri { get; set; }
    }
}
