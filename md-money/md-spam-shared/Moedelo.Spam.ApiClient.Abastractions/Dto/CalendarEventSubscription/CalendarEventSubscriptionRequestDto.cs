using System;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Common;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription
{
    public class CalendarEventSubscriptionRequestDto : BasePagedListRequest
    {
        /// <summary>
        /// Если указан, то только подписки, со значением PaymentStartDateBefore меньше или равным указанному значению
        /// </summary>
        public DateTime? PaymentStartDateBefore { get; set; }

        /// <summary>
        /// Если указан, то только подписки, со значением PaymentExpiryDate больше или равным указанному значению
        /// </summary>
        public DateTime? PaymentExpiryDateAfter { get; set; }

        /// <summary>
        /// Если указан, то только подписки, со значением PaymentExpiryDate меньше или равным указанному значению
        /// </summary>
        public DateTime? PaymentExpiryDateBefore { get; set; }

        /// <summary>
        /// Если указан, то только подписки, со значением LastEmailProcessDateBefore меньше или равным указанному значению
        /// </summary>
        public DateTime? LastEmailProcessDateBefore { get; set; }

        /// <summary>
        /// Если указан, то только подписки, со значением LastSmsProcessDateBefore меньше или равным указанному значению
        /// </summary>
        public DateTime? LastSmsProcessDateBefore { get; set; }

        public bool? NoticeByMail { get; set; }

        public bool? NoticeBySms { get; set; }
    }
}