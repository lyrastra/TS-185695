using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.PaymentBills
{
    public class PaymentsAndBillsCreationRequestDto
    {
        /// <summary>
        /// заявки на создание платежей
        /// </summary>
        public IReadOnlyCollection<PaymentAndBillCreationRequestDto> Payments { get; set; }
        /// <summary>
        /// это платежи по рассрочке
        /// </summary>
        public bool IsInstalmentPlan { get; set; }

        /// <summary>
        /// Признак триального платежа
        /// </summary>
        public bool IsTrial { get; set; }
    }
}