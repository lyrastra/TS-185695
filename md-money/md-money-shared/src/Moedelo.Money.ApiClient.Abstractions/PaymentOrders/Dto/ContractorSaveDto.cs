namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
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
        public string Name { get; set; }

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
