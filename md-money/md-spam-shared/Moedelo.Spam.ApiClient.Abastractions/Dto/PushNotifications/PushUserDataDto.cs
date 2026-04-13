using System;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications
{
    public class PushUserDataDto<T> where T : IPushNotificationData
    {
        /// <summary>Тип пуша: task_new_comment, info, chat_new_message... (обязательное поле)</summary>
        public string Type { get; set; }

        /// <summary>Id любых данных внутри пуша </summary>
        public string Id { get; set; }

        /// <summary>
        /// Может ли пуш быть отложен (например, если у пользователя ночь)
        /// Если заполнен этот параметр, то пуши отправляются с учётом таймзоны клиента с 8:00 до 22:00
        /// </summary>
        public bool CanBeDeffered { get; set; }

        /// <summary>
        /// Предпочтительная дата и время отправки push (работает совместно с параметром CanBeDeffered = true)
        /// </summary>
        /// <remarks>
        /// Рекомендации:
        /// <para>Время в параметре PreferredSendDate нужно заполнять без учета таймзоны клиента</para>
        /// Пример:
        /// <para>
        /// В параметре PreferredSendDate переданы дата следующий день и время 9:00, у клиента таймзона +1 от мск <br/>
        /// Отправка сработает на следующий день в 8:00 по мск, в это время у клиента будет 9:00
        /// </para>
        /// </remarks>
        public DateTime? PreferredSendDate { get; set; }

        /// <summary>
        /// Надо ли слать смс, если пользоватеть не посмотрел пуш в течение какого-то кол-ва времени
        /// </summary>
        public bool IsDeliveryRequired { get; set; }

        /// <summary>
        /// Название типа модели данных для пуша
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// Json с данными под текущий шаблон
        /// </summary>
        public string Data { get; set; }
    }
}