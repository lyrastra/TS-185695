namespace Moedelo.AccountV2.Dto.User
{
    public class ChangeLoginRequestDto
    {
        public string OldLogin { get; set; }

        public string NewLogin { get; set; }

        public bool IsConfirmationEmail { get; set; }

        public string ConfirmationGuid { get; set; }

        public bool IsWithoutConfirmEmail { get; set; }

        public string CurrentPasswordMd5 { get; set; }

        public string Host { get; set; }
    }
}