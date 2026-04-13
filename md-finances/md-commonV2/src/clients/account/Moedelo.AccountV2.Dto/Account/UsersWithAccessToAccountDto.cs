namespace Moedelo.AccountV2.Dto.Account
{
    public class UsersWithAccessToAccountDto
    {
        public bool CanAddUsers { get; set; }

        public ListWithTotalCount<AccountUserAccessDto> UserAccesses { get; set; }
    }

    public class AccountUserAccessDto
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int FirmsCount { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanDesignateAccountAdmin { get; set; }
        public bool CanChangeRole { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLegalUser { get; set; }
    }
}
