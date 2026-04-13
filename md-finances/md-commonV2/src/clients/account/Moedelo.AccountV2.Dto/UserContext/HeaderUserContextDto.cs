using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.AccountV2.Dto.UserContext
{
    public class HeaderUserContextDto
    {
        public int FirmId { get; set; }

        public string FirmName { get; set; }

        public string Login { get; set; }

        public string TariffShortName { get; set; }

        public DateTime TariffExpirationDate { get; set; }

        public DateTime TotalExpirationDate { get; set; }

        public bool IsPaid { get; set; }

        public string RegionCode { get; set; }

        public List<AccessRule> RightList { get; set; }

        public string PaymentMethod { get; set; }

        public AccountantCompanyType OutsourceCompanyType { get; set; }

        public bool IsToggleToMainFirm { get; set; }

        public string UserUid { get; set; }

        public bool IsOoo { get; set; }

        public bool IsEmployeeMode { get; set; }

        public bool IsManualCashMode { get; set; }

        [Obsolete("Это поле больше не заполняется и будет скоро удалено")]
        public List<FirmBalanceDto> BalanceList { get; set; }

        public bool IsMultiFirmMode { get; set; }

        public bool IsProfOutsourceUser { get; set; }

        public string UserRoleName { get; set; }

        public string ProfOutsourceAccountName { get; set; }

        public bool IsHasAccount { get; set; }

        public bool IsAccountAdmin { get; set; }

        public DateTime? RegistrationInService { get; set; }

        public int? SourceFirmId { get; set; }
    
        public int? TargetFirmId { get; set; }
    }
}