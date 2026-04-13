using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Models
{
    internal class PaymentLinksCreationResponse
    {
        /// <summary>
        /// Предыдущие связи со счетами
        /// </summary>
        public IReadOnlyCollection<long> PreviousBillBaseIds { get; set; }
    }
}
