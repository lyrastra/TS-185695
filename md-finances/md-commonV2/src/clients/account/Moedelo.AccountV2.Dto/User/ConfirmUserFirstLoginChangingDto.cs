using System;
namespace Moedelo.AccountV2.Dto.User
{
    /// <summary>
    /// Запрос на использование кода подтверждение смены логина
    /// </summary>
    public class ConfirmUserFirstLoginChangingDto
    {
        /// <summary>
        /// Хост, на который пришёл запрос
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// IP-адрес клиента
        /// </summary>
        public string RequestIpAddress { get; set; }
        /// <summary>
        /// Уникальный идентификатор действия
        /// </summary>
        public Guid ActionId { get; set; }
        /// <summary>
        /// Предъявленный код подтверждения
        /// </summary>
        public string ConfirmationCode { get; set; }
        /// <summary>
        /// Дата и время получения подтверждения
        /// </summary>
        public DateTime ConfirmationDate { get; set; }
    }
}