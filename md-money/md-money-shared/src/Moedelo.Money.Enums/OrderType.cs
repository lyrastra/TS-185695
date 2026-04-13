namespace Moedelo.Money.Enums
{
    /// <summary>
    /// Устаревший тип для поддержки старого кода
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Платежное поручение
        /// </summary>
        PaymentOrder = 0,

        /// <summary>
        /// Инкассовое поручение
        /// </summary>
        CollectorOrder = 1,

        /// <summary>
        /// Банковский ордер: Списана комиссия банка
        /// </summary>
        MemorialWarrantBankFeeIsDeducted = 2,

        /// <summary>
        /// Мемориальный ордер: Объявление на взнос наличными
        /// </summary>
        MemorialWarrantReceiptFromCash = 3,

        /// <summary>
        /// Денежный чек: Снятие с расчетного счета
        /// </summary>
        MemorialWarrantReceivedCashInBank = 4,

        /// <summary>
        /// Бюджетный платеж
        /// </summary>
        BudgetaryPayment = 5,
    }
}