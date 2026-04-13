using System;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class GroupedPaymentHistoryDto
    {
        public int FirstPaymentId { get; set; }

        public int PrimaryPaymentId { get; set; }

        /// <summary> Имя тарифа </summary>
        public string TariffName { get; set; }

        /// <summary> Название аутсорс тарифа </summary>
        public string OutsourceTariffName { get; set; }

        /// <summary> Сумма всех частей </summary>
        public decimal Summ { get; set; }

        /// <summary> Дата первого платежа </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary> Дата начала первого платежа </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата окончания последнего платежа </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary> Дата прихода денег </summary>
        public DateTime? IncomingDate { get; set; }
    }
}
