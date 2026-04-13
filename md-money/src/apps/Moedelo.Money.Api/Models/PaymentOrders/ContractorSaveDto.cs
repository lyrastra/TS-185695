using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders
{
    /// <summary>
    /// Контрагент
    /// </summary>
    public class ContractorSaveDto
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название (ФИО)
        /// </summary>
        [RequiredValue]
        [ValidateXss]
        [ContractorName]
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
        /// Номер расчётного счёта
        /// </summary>
        [ValidateXss]
        public string SettlementAccount { get; set; }

        /// <summary>
        /// Название банка
        /// </summary>
        [ValidateXss]
        public string BankName { get; set; }

        /// <summary>
        /// БИК банка
        /// </summary>
        [ValidateXss]
        public string BankBik { get; set; }

        /// <summary>
        /// Корреспондентский счет банка
        /// </summary>
        [ValidateXss]
        public string BankCorrespondentAccount { get; set; }
    }
}
