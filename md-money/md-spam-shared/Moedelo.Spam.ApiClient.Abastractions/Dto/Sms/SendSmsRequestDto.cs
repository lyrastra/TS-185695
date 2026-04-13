using System;
using Moedelo.Spam.ApiClient.Abastractions.Enums.Common;
using Moedelo.Spam.ApiClient.Abastractions.Enums.Sms;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.Sms
{
    public class SendSmsRequestDto
    {
        public string Number { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Обязателен, если CanBeDeferred = true
        /// </summary>
        public int? FirmId { get; set; }

        /// <summary>
        /// Может ли смс быть отложено (например, если у пользователя ночь).
        /// Если true, FirmId обязателен.
        /// Если заполнен этот параметр, то пуши отправляются с учётом таймзоны клиента в разрешенное время
        /// </summary>
        public bool CanBeDeferred { get; set; }

        /// <summary>
        /// Предпочтительная дата и время отправки (работает совместно с параметром CanBeDeffered = true)
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

        public string SentFromAppModule { get; set; }

        public WLProductPartition ProductPartition { get; set; }

        /// <summary>
        /// обычная строка или json индивидуальные параметры каждого WL
        /// </summary>
        public string WLSpecialParams { get; set; }

        /// <summary>
        /// Тип смс, в каких целях используется отправка СМС (не обязательное поле)
        /// </summary>
        public SmsType? SmsType { get; set; }
    }
}
