using System;

namespace Moedelo.BillingV2.Dto.FrameInfo
{
    /// <summary> Информация о биллинге фирмы для фрейма </summary>
    public class BillingFrameInfoForFirmResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Статус текущей оплаты </summary>
        public string CurrentPayedStatus { get; set; }

        /// <summary> Дата начала текущей оплаты </summary>
        public DateTime? CurrentPaymentStartDate { get; set; }

        /// <summary> Дата окончания текущей оплаты </summary>
        public DateTime? CurrentPaymentEndDate { get; set; }

        /// <summary> Продукт текущей оплаты </summary>
        public string CurrentPaymentProduct { get; set; }

        /// <summary> Статус покупки разовых услуг </summary>
        public bool HasPurchasedOneTimeServices { get; set; }
    }
}