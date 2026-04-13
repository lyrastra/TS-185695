using System;
using Moedelo.BillingV2.Dto.BillingInfo.enums;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class GeneralRenewSubscriptionsReportRowDto
    {
        /// <summary>
        /// фирма, по которой сделан этот платёж
        /// </summary>
        public int FirmId { get; set; }
        /// <summary>
        /// Платёж, который устаревает в указанном отчётном периоде
        /// Основание для переподписки данного клиента
        /// </summary>
        public PaymentInfo ExpiringPayment { get; set; }
        /// <summary>
        /// "Ближайший" платёж, действие которого оканчивается после устаревающего платежа (ExpiringPayment)
        /// Если этот платёж присутствует, значит переподписка по основанию ExpiringPayment уже произошла 
        /// </summary>
        public PaymentInfo RenewingPayment { get; set; }
        /// <summary>
        /// есть ли у фирмы покупки по нескольких продуктам (более одного) с начала запрошенного периода отчёта
        /// </summary>
        public bool FirmHasManyPaidProductsSinceStartDate { get; set; }
        /// <summary>
        /// Общее количество оплат по фирме за всё время (исключая допродажи и покупки одноразовых услуг)
        /// </summary>
        public int FirmTotalSubscriptionPaymentsCount { get; set; }
        /// <summary>
        /// год обслуживания фирмы
        /// Количество неполных лет со дня начала первой оплаты по дату начала периода выгрузки отчета
        /// с округлением до целого вверх)
        /// </summary>
        public int FirmYearOrService { get; set; }

        public class PaymentInfo
        {
            /// <summary>
            /// номер платежа
            /// </summary>
            public int PaymentId { get; set; }
            /// <summary>
            /// метод оплаты платежа
            /// </summary>
            public string PaymentMethod { get; set; }
            /// <summary>
            /// продукт, к которому относится предмет/объект покупки
            /// </summary>
            public string Product { get; set; }
            /// <summary>
            /// дата начала действия платежа
            /// </summary>
            public DateTime StartDate { get; set; }
            /// <summary>
            /// дата окончания действия платежа
            /// </summary>
            public DateTime EndDate { get; set; }
            /// <summary>
            /// Дата создания платежа (dbo.PaymentHistory::Date)
            /// </summary>
            public DateTime? Date { get; set; }
            /// <summary>
            /// Дата поступления денежных средств по платежу
            /// </summary>
            public DateTime? IncomingDate { get; set; }
            /// <summary>
            /// Идентификатор тарифа, по которому осуществлена покупка
            /// ВНИМАНИЕ: неактуально для платежей из нового биллинга
            /// </summary>
            public int PurchaseTariffId { get; set; }
            /// <summary>
            /// Название предмета/объекта покупки
            /// </summary>
            public string PurchaseName { get; set; }
            /// <summary>
            /// идентификатор продавца 
            /// </summary>
            public int SellerId { get; set; }
            /// <summary>
            /// Сумма счета, выставленная клиенту по данному платежу (итоговая для клиента после всех скидок и т.п.)
            /// </summary>
            public decimal FinalSum { get; set; }
            /// <summary>
            /// Нормативная сумма счёта (согласно прайс-листу до скидок, коэффициэнтов и пр.)
            /// </summary>
            public decimal NormativeSum { get; set; }
        }

        /// <summary>
        /// Подключено ли автопродление
        /// </summary>
        public AutoRenewalSettingsStatus? AutoRenewalSettingsStatus { get; set; }

        /// <summary>
        /// Следующая дата выставления счёта на продление
        /// </summary>
        public DateTime? AutoRenewalNextDate { get; set; }
    }
}