namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class ContractorDto
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
        /// Форма контрагента
        /// 1 - Юр. лицо
        /// 2 - ИП
        /// 3 - Физ. лицо
        /// 4 - Нерезидент
        /// </summary>
        public int? Form { get; set; }

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