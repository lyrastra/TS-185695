using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    /// <summary>
    /// Реквизиты получателя
    /// </summary>
    public class BudgetaryRecipientSaveDto
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
        /// ОКАТО
        /// </summary>
        [ValidateXss]
        public string Okato { get; set; }

        /// <summary>
        /// ОКТМО
        /// </summary>
        [Oktmo]
        [ValidateXss]
        public string Oktmo { get; set; }

        /// <summary>
        /// Номер расчётного счёта
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
        /// Корреспондентский счет банка
        /// </summary>
        [ValidateXss]
        [SettlementAccount]
        public string BankCorrespondentAccount { get; set; }
    }
}
