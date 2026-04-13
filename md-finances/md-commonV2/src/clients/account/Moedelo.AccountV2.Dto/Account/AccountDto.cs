using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AccountV2.Dto.Account
{
    /// <summary>
    /// Аккаунт в системе Моё Дело
    /// </summary>
    public class AccountDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя (dbo.Users), являющегося главным пользователем (владельцем) этого аккаунта 
        /// </summary>
        public int MainAdminId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Тип аккаунта
        /// </summary>
        public AccountType Type { get; set; }

        public int CreateUserId { get; set; }
    }
}