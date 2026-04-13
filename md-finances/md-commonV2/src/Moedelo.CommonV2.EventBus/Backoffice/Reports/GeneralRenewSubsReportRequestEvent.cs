using System;

namespace Moedelo.CommonV2.EventBus.Backoffice.Reports
{
    // класс должен называться как-то типа GeneralRenewSubscriptionsReportOrderEvent, но неожиданно где-то происходит
    // превышение максимальной длины чего-то там в недрах rabbitmq
    public class GeneralRenewSubsReportRequestEvent
    {
        public Guid RequestGuid { get; set; }
        /// <summary>
        /// логин пользователя, запросившего отчёт
        /// </summary>
        public string RequestedBy { get; set; }
        /// <summary>
        /// время, когда был запрошен отчёт
        /// </summary>
        public DateTime RequestedAt { get; set; }
        /// <summary>
        /// email, на который надо отправить результат
        /// </summary>
        public string EmailTo { get; set; }

        /// <summary>
        /// дата начала периода переподписки
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// дата завершения периода переподписки
        /// </summary>
        public DateTime DateUntil { get; set; }
        /// <summary>
        /// перечень продуктов, по которым должен быть построен отчёт
        /// </summary>
        public string[] Products { get; set; }
        /// <summary>
        /// типы подписок, которые должны попасть в отчёт
        /// </summary>
        public SubscriptionType[] Subscriptions { get; set; }
        /// <summary>
        /// фильтр по аутсорсеру
        /// </summary>
        public OutsourcerType[] OutsourcerFilter { get; set; }
        /// <summary>
        /// Перечень отделов сопровождения, по которым должны быть собраны данные
        /// </summary>
        public int[] SupportDepartments { get; set; }
        /// <summary>
        /// Методы, платежи с которыми не должны попадать в отчёт
        /// </summary>
        public string[] ExcludedPaymentMethods { get; set; }
        
        public enum OutsourcerType
        {
            /// <summary>
            /// не АУТ (без аутсорсера)
            /// </summary>
            NotOut = 0,
            /// <summary>
            /// Главучёт
            /// </summary>
            Glavuchet = 1,
            /// <summary>
            /// не Главучёт
            /// </summary>
            NotGlavuchet = 2
        }
        
        public enum SubscriptionType
        {
            /// <summary>
            /// собственные
            /// </summary>
            Own = 1,
            /// <summary>
            /// банковские
            /// </summary>
            Bank = 2,
            /// <summary>
            /// партнёрские
            /// </summary>
            Partnerial = 4,
            /// <summary>
            /// все
            /// </summary>
            All = Own | Bank | Partnerial,
        }
    }
}