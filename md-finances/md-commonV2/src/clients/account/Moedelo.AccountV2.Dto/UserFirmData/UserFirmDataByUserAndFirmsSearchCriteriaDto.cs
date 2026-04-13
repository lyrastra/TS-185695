using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.UserFirmData
{
    public class UserFirmDataByUserAndFirmsSearchCriteriaDto
    {
        public int UserId { get; set; }
        public IReadOnlyCollection<int> FirmIds { get; set; }
    }
}
