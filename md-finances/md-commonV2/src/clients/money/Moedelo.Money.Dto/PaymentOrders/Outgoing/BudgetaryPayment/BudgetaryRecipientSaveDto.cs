namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment
{
    /// <summary>
    /// Реквизиты получателя
    /// </summary>
    public class BudgetaryRecipientSaveDto
    {
        /// <summary>
        /// Получатель
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
        /// ОКАТО
        /// </summary>
        public string Okato { get; set; }

        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; }

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