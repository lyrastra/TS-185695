using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    /// <summary>
    /// Реквизиты получателя
    /// </summary>
    public class UnifiedBudgetaryRecipientSaveDto
    {
        /// <summary>
        /// Получатель
        /// </summary>
        [ValidateXss]
        [RequiredValue]
        [BudgetaryRecipient]
        public string Name { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [ValidateXss]
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        [ValidateXss]
        public string Kpp { get; set; }

        /// <summary>
        /// ОКТМО
        /// </summary>
        [Oktmo(allowZero: true)]
        [ValidateXss]
        public string Oktmo { get; set; }

        /// <summary>
        /// Казначейский счёт
        /// </summary>
        [ValidateXss]
        [SettlementAccount]
        public string SettlementAccount { get; set; }

        /// <summary>
        /// Название банка
        /// </summary>
        [ValidateXss]
        [RequiredValue]
        public string BankName { get; set; }

        /// <summary>
        /// БИК банка
        /// </summary>
        [Bik]
        [ValidateXss]
        [RequiredValue]
        public string BankBik { get; set; }

        /// <summary>
        /// Единый казначейский счёт
        /// </summary>
        [ValidateXss]
        [SettlementAccount]
        public string BankCorrespondentAccount { get; set; }
    }
}
