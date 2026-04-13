using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using System;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger
{
    /// <summary>
    /// Класс, хранящий залогированное событие (которое ещё не успело отправиться в базу)
    /// </summary>
    public class EventToLog
    {
        public EventToLog()
        {
            // Время создания события = время, кое попадёт в лог
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Какой метод был вызван (понятный человеку уникальный ID метода)
        /// </summary>
        public string MethodId { get; set; }

        /// <summary>
        /// Тип события
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Сериализованное сообщение из исключения
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Запрос
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Ответ
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Время, в которое событие поступило в логгер
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Длительность запроса
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Пользователь, под которым произошло событие (может быть не задан, если событие сгенерировано в консоли, например)
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Фирма, под которой произошло событие (может быть не задана, если событие сгенерировано в консоли, например)
        /// </summary>
        public int? FirmId { get; set; }
    }
}
