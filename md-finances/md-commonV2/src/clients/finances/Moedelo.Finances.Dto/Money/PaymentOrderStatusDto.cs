using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Finances.Client.Money.Dtos
{
    public class PaymentOrderStatusDto
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Платёжное получение оплачено
        /// </summary>
        public bool IsPaid { get; set; }
    }
}