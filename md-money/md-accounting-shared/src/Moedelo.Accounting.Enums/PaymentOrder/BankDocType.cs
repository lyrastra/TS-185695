namespace Moedelo.Accounting.Enums.PaymentOrder
{
    public enum BankDocType
    {
        NotDefine = 0,

        /// <summary>
        /// Списано, зачислено по платежному поручению, по поручению банка
        /// </summary>
        PaymentOrder = 1,

        /// <summary>
        /// Оплачено, зачислено по платежному требованию
        /// </summary>
        MemorialWarrant = 2,

        BudgetaryPayment = 3,

        IncomingFromCash = 4,

        PaymentRequest = 9,

        MoneyOrder = 5,

        CollectionOrder = 6,

        SettlementCheck = 7,

        OpeningOfLetterOfCredit = 8,

        /// <summary>
        /// Списано, зачислено по платежному ордеру
        /// </summary>
        PaymentWarrant = 16,

        /// <summary>
        /// Списано, зачислено по банковскому ордеру
        /// </summary>
        BankService = 17,

        /// <summary>
        /// Списано, зачислено по ордеру по передаче ценностей
        /// </summary>
        TransferValuesWarrant = 18,
    }
}