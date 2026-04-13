using System.Collections.Generic;

namespace Moedelo.Accounts.Abstractions.Dto.UserFirmData;

public class UserFirmDataByUserAndFirmsSearchCriteriaDto
{
    public int UserId { get; set; }
    public IReadOnlyCollection<int> FirmIds { get; set; }
}