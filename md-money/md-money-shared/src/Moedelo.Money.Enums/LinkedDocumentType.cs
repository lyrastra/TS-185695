namespace Moedelo.Money.Enums
{
    public enum LinkedDocumentType
    {
        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 1,

        /// <summary>
        /// Счет-фактура
        /// </summary>
        Invoice = 2,

        /// <summary>
        /// Бухгалтерская справка
        /// </summary>
        AccountingStatement = 4,

        /// <summary>
        /// Платежное поручение
        /// </summary>
        PaymentOrder = 5,

        /// <summary>
        /// Акт
        /// </summary>
        Statement = 6,

        /// <summary>
        /// Приходный кассовый ордер
        /// </summary>
        IncomingCashOrder = 9,

        /// <summary>
        /// Расходный кассовый ордер
        /// </summary>
        OutcomingCashOrder = 10,

        /// <summary>
        /// Авансовый отчет
        /// </summary>
        AdvanceStatement = 11,

        /// <summary>
        /// Счет
        /// </summary>
        Bill = 13,

        /// <summary>
        /// Договор
        /// </summary>
        Project = 14,

        /// <summary>
        /// Инвентарная карточка ОС
        /// </summary>
        InventoryCard = 18,

        /// <summary>
        /// Отчет посредника
        /// </summary>
        MiddlemanReport = 25,

        /// <summary>
        /// Банковская операция по электронным деньгам
        /// </summary>
        PurseOperation = 31,

        /// <summary>
        /// Входящий универсальный передаточный документ
        /// </summary>
        Upd = 33,
        
        /// <summary>
        /// Воврат розничной продажи
        /// </summary>
        RetailRefund = 34,

        /// <summary>
        /// Исходящий универсальный передаточный документ
        /// </summary>
        SalesUpd = 36,

        /// <summary>
        /// Основной договор в учетке
        /// </summary>
        MainContract = 52,

        /// <summary>
        /// Акт приема-передачи
        /// </summary>
        ReceiptStatement = 54,

        /// <summary>
        /// Исходящий инвойс
        /// </summary>
        SalesCurrencyInvoice = 55,

        /// <summary>
        /// Входящий инвойс
        /// </summary>
        PurchaseCurrencyInvoice = 56,

        /// <summary>
        /// Резервный документ для платежей
        /// Эмулирует привязанный подтверждающий документ
        /// </summary>
        PaymentReserve = 60,

        /// <summary>
        /// Дочерний бюджетный платеж для ЕНП
        /// </summary>
        UnifiedBudgetarySubPayment = 61,
    }
}
