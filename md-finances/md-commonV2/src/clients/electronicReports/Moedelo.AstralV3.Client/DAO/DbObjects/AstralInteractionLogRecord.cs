using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using System;

namespace Moedelo.AstralV3.Client.DAO.DbObjects
{
    /// <summary>
    /// Запись лога астрального клиента
    /// </summary>
    public class AstralInteractionLogRecord
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Какому методу соответствует лог (ссылка на таблицу dbo.AstralInteractionMethods)
        /// </summary>
        public int MethodId { get; set; }

        /// <summary>
        /// Фирма (nullable, т.к. при вызове из консоли юзера и фирмы нет)
        /// </summary>
        public int? FirmId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// В какой момент произошло залогированное событие
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// Тип события (что-то вроде уровня severity)
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Если произошло исключение, то здесь будет сохранена его сериализация
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Залогированный запрос
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Залогированный ответ
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Длительность запроса в миллисекундах
        /// </summary>
        public int Duration { get; set; }
    }
}
