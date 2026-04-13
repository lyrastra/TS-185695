using System;

namespace Moedelo.AccountV2.Dto.UserContext
{
    public class UserContextBasicInfoDto
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public string Login { get; set; }

        public string UserName { get; set; }

        public string OrganizationName { get; set; }

        public bool IsInternal { get; set; }

        public string Inn { get; set; }

        public bool IsOoo { get; set; }

        public bool IsEmployerMode { get; set; }

        public DateTime? FirmRegistrationDate { get; set; }
    }
}