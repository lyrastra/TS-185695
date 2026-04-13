using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Dto.UserContext
{
    public class UserCommonContextDto
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public string Login { get; set; }

        public string UserName { get; set; }

        public string OrganizationName { get; set; }

        public string RoleCode { get; set; }

        public string RoleName { get; set; }

        public ISet<AccessRule> AccessRules { get; set; }

        public string Inn { get; set; }

        public bool IsOoo { get; set; }

        public bool IsEmployerMode { get; set; }

        public DateTime? FirmRegistrationDate { get; set; }

        public bool IsTrialCard { get; set; }

        public bool IsTrial { get; set; }

        public bool IsPaid { get; set; }

        public bool IsExpired { get; set; }

        public string ProductPlatform { get; set; }

        public string ProductGroup { get; set; }

        public bool IsIpOrOooRegistrationTariff { get; set; }

        public int TariffId { get; set; }

        public string TariffName { get; set; }

        public bool IsInternal { get; set; }
    }
}