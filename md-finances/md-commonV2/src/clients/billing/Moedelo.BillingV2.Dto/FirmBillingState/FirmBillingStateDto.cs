using System;

namespace Moedelo.BillingV2.Dto.FirmBillingState
{
    public class FirmBillingStateDto
    {
        public enum FirmPaidStatus
        {
            /// <summary>
            /// у фирмы нет ни одного платежа
            /// </summary>
            NoOnePayment = 1,
            /// <summary>
            /// находится на действующем триальном платеже
            /// </summary>
            Trial = 2,
            /// <summary>
            /// находится на действующем нетриальном платеже
            /// </summary>
            Paid = 3,
            /// <summary>
            /// нет действующего платежа, но есть платёж, действие которого окончено
            /// </summary>
            Expired = 4
        }

        public int FirmId { get; set; }
        /// <summary>
        /// Статус оплаты
        /// </summary>
        public FirmPaidStatus PaidStatus { get; set; }
        public string ProductPlatform { get; set; }
        /// <summary>
        /// Это поле оставлено для обратной совместимости. Может быть использовано только для анализа тарифов старого биллинга.
        /// В новом биллинге понятие тариф стало размытым и по его идентификатору нельзя ничего определить.
        /// </summary>
        public int[] TariffIds { get; set; } = Array.Empty<int>();
        public string TariffName { get; set; }
        /// <summary>
        /// Дата начала действия текущего платежа
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Дата окончания действия текущего платежа
        /// </summary>
        public DateTime ValidUntil { get; set; }
        /// <summary>
        /// перечень продуктовых групп, по которым имеются действующие платежи
        /// надеюсь, что это поле не нужно
        /// </summary>
        public string[] ProductGroups { get; set; } = Array.Empty<string>();
        // оставлено для поддержки obsolete поля IUserContext::IsTrialCard
        public bool IsTrialCard { get; set; }
    }
}