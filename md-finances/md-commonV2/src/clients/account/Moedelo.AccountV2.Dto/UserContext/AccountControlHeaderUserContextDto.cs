using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Dto.UserContext
{
    public class AccountControlHeaderUserContextDto
    {
        public string Login { get; set; }

        public string FirmName { get; set; }

        public string UserUid { get; set; }

        public bool IsMultiFirmMode { get; set; }

        public bool IsProfOutsourceUser { get; set; }

        public bool IsAccountAdmin { get; set; }

        public List<AccessRule> ExplicitRuleList { get; set; }
    }
}