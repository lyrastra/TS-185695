using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models
{
    public class ConfirmContractorDto
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название (ФИО)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип контрагента
        /// </summary>
        public ContractorType ContractorType { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Номер расчётного счёта
        /// </summary>
        public string SettlementAccount { get; set; }

        /// <summary>
        /// Название банка
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// БИК банка
        /// </summary>
        public string BankBik { get; set; }

        /// <summary>
        /// Корреспондентский счет банка
        /// </summary>
        public string BankCorrespondentAccount { get; set; }
    }
}