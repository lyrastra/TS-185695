namespace Moedelo.AccountV2.Dto.User
{
    public class UserLoginDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public bool IsUserExist { get; set; }
    }
}
