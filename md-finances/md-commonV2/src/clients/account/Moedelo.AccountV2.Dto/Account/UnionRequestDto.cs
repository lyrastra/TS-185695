using System;
using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AccountV2.Dto.Account
{
    public class UnionRequestDto
    {
        /// <summary>
        /// Идентификатор запроса на объединение
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя запросившего объединение
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Логин пользователя запросившего объединение
        /// </summary>
        public string SenderLogin { get; set; }

        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Логин главного пользователя
        /// </summary>
        public string MainLogin { get; set; }

        /// <summary>
        /// Строковое перечисление присоединенных логинов
        /// </summary>
        public string LinkedLogins { get; set; }

        /// <summary>
        /// Статус обработки запроса на объединение
        /// </summary>
        public UnionRequestStatus RequestStatus { get; set; }
    }
}