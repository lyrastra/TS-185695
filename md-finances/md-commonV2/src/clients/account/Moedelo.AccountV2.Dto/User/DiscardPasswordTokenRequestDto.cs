using System;

namespace Moedelo.AccountV2.Dto.User
{
    public class DiscardPasswordTokenRequestDto
    {
        public int UserId { get; set; }

        /// <summary>
        /// Дата до которой guid сброса пароля будет считаться активным (рабочим)
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }
}