namespace Moedelo.CashV2.Dto.YandexKassa.PaymentImport
{
    public class OrderDetailsDto
    {
        /// <summary>
        /// Имя
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
        /// Номер расчетного счета
        /// </summary>
        public string SettlementNumber { get; set; }

        /// <summary>
        /// БИК банка
        /// </summary>
        public string BankBik { get; set; }

        /// <summary>
        /// Полное наименование банка
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Кор.счет банка
        /// </summary>
        public string BankCorrespondentAccount { get; set; }


        /// <summary> Город Банка </summary>
        public string BankCity { get; set; }

        /// <summary>
        /// Организация или индивидуальный предприниматель
        /// </summary>
        public bool IsOoo { get; }

        /// <summary>
        /// Адрес для квитанции
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// ОКАТО
        /// </summary>
        public string Okato { get; set; }

        /// <summary>
        /// ОКTMO
        /// </summary>
        public string Oktmo { get; set; }
    }
}