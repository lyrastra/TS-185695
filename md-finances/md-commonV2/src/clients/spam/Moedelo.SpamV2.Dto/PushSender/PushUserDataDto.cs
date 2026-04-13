using System;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Dto.PushSender
{
    public class PushUserDataDto<T> where T : IPushNotificationData
    {
        public string Type { get; set; }

        public string Id { get; set; }

        public bool CanBeDeffered { get; set; }

        /// <summary>
        /// Предпочтительная дата и время отправки push (работает совместно с параметром CanBeDeffered = true)
        /// </summary>
        public DateTime? PreferredSendDate { get; set; }

        public bool IsDeliveryRequired { get; set; }

        public string DataTypeName { get; set; }

        public string Data { get; set; }

    }
}