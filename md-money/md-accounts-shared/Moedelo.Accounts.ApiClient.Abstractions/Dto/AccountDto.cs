using Moedelo.Accounts.ApiClient.Enums;

namespace Moedelo.Accounts.Abstractions.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }

        /// <summary>
        /// идентификатор пользователя (dbo.Users), являющегося главным пользователем (владельцем) этой фирмы 
        /// </summary>
        public int MainAdminId { get; set; }

        public string Name { get; set; }

        public AccountType Type { get; set; }

        public int CreateUserId { get; set; }
    }
}