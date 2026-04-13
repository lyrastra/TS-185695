using System;

namespace Moedelo.AccountV2.Dto.ExternalApi
{
    public class AuthorizedApiKeyDto
    {
        /// <summary>
        /// Когда был создан ключ
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Аккаунт, к которому привязан ключ
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Идентификатор пользователя (специальный пользователь для API)
        /// </summary>
        public int ApiUserId { get; set; }
        /// <summary>
        /// Идентификатор пользователя, создавшего ключ
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// Идентификатор фирмы, для которой выпущен ключ
        /// </summary>
        public int FirmId { get; set; }
    }
}
