using System;

namespace Moedelo.Estate.Kafka.Abstractions.Events.EventData.InventoryCard
{
    public class InventoryCardTaxDescription
    {
        /// <summary>
        /// Подлежит гос. регистрации
        /// </summary>
        public bool NeedStateRegistration { get; set; }

        /// <summary>
        /// Дата гос. регистрации
        /// </summary>
        public DateTime? DateOfStateRegistration { get; set; }

        /// <summary>
        /// Дата ввода в эксплуатацию
        /// </summary>
        public DateTime? CommissioningDate { get; set; }

        /// <summary>
        /// Срок полезного использования (в месяцах)
        /// </summary>
        public int UsefulLife { get; set; }

        /// <summary>
        /// Амортизация введенная в остатках
        /// </summary>
        public decimal AmortizationInBalance { get; set; }

        /// <summary>
        /// Первоначальная стоимость в НУ
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Оплачено в остатках
        /// </summary>
        public decimal? PaidCost { get; set; }
        
        /// <summary>
        /// Дата оплаты в остатках
        /// </summary>
        public DateTime? PaidDate { get; set; }
    }
}