namespace Moedelo.AccountV2.Dto.Account
{
    public class MergeAccountsRequestDto
    {
        /// <summary>
        /// логин пользователя, в аккаунт которого добавляются данные из другого аккаунта
        /// </summary>
        public string MainAccountUserLogin { get; set; }

        /// <summary>
        /// логин пользователя, из аккаунта которого все данные переносятся в другой аккаут 
        /// </summary>
        public string AccountToMergeUserLogin { get; set; }

        /// <summary>
        /// идентификатор пользователя партнёрки, который производит изменения
        /// </summary>
        public int AuthorUserId { get; set; }

        /// <summary>
        /// IP-адрес, с которого был получен запрос на удаление
        /// </summary>
        public string AuthorIpAddress { get; set; }
    }
}
