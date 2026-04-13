using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.UserContext
{
    // старый класс для старого v1 метода
    public class UserContextForHeaderDto
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public string Login { get; set; }

        public string OrgName { get; set; }

        public bool IsOoo { get; set; }

        public bool IsEmployerMode { get; set; }

        public bool IsManualCashMode { get; set; }

        public string RegionCode { get; set; }

        public List<int> RightList { get; set; }

        public List<UserPaymentExtendedDto> PaymentList { get; set; } 
    }
}
