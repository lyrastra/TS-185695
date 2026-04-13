namespace Moedelo.Accounts.Abstractions.Dto.FirmUsers;

public class UsersWithAccessToFirmDto
{
    public bool CanAddUsers { get; set; }

    public ListWithTotalCount<FirmUserAccessDto> UserAccesses { get; set; }
}