namespace Moedelo.Common.Enums.Enums.Documents
{
    public enum AccountingDocumentType
    {
        Default = 0,

        /// <summary>
        /// Накладная
        /// </summary>
        Waybill = 1,
        
        /// <summary>
        /// Счет-фактура
        /// </summary>
        Invoice = 2,

        /// <summary>
        /// Складской документ
        /// </summary>
        StockDocument = 3,

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
        /// Возврат от покупателя
        /// </summary>
        ReturnFromBuyer = 7,

        /// <summary>
        /// Входящий акт приёма-передачи ОС
        /// </summary>
        IncomingStatementOfFixedAsset = 8,

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
        AccountingAdvanceStatement = 11,

        /// <summary>
        /// Отчет о розничной продаже
        /// </summary>
        RetailReport = 12, 

        /// <summary>
        /// Счет
        /// </summary>
        Bill = 13,

        /// <summary>
        /// Договор
        /// </summary>
        Project = 14,

        FixedAssetClosedPeriod = 15,

        Salary = 16,

        /// <summary>
        /// Возврат поставщику
        /// </summary>
        ReturnToSupplier = 17,
        
        /// <summary>
        /// Инвентарная карточка ОС
        /// </summary>
        InventoryCard = 18,

        /// <summary> 
        /// Вложение во внеоборотные активы 
        /// </summary>
        FixedAssetInvestment = 19,

        /// <summary> 
        /// Инвентаризация
        /// </summary>
        Inventory = 20,

        /// <summary>
        /// Финансовая операция
        /// </summary>
        FinancialOperation = 21,

        /// <summary>
        /// Командировка
        /// </summary>
        BusinessTrip = 22,

        /// <summary>
        /// Подтверждающий документ в авансовом отчете в бизе при отключенном складе
        /// </summary>
        AdvanceStatementRelatedDocument = 23,

        /// <summary>
        /// Посреднеческий договор
        /// </summary>
        MiddlemanContract = 24,

        /// <summary>
        /// Отчет посредника
        /// </summary>
        MiddlemanReport = 25,

        /// <summary>
        /// Требование-накладная
        /// </summary>
        RequisitionWaybill = 26,

        /// <summary>
        /// Опереция перемешения между складами
        /// </summary>
        StockMovement = 27,

        /// <summary>
        /// Условный документ для учета начислений в фонды из зарплаты
        /// </summary>
        SalaryFundCharge = 28,

        /// <summary>
        /// Условный документ для учета начислений по НДФЛ из зарплаты
        /// </summary>
        SalaryNdfl = 29,

        /// <summary>
        /// Условный документ для учета начислений по зарплате
        /// </summary>
        SalaryCharge = 30,

        /// <summary>
        /// Банковская операция по электронным деньгам
        /// </summary>
        PurseOperation = 31,

        /// <summary>
        /// Складская операция комплектации
        /// </summary>
        StockBundling = 32,

        /// <summary>
        /// Входящий универсальный передаточный документ
        /// </summary>
        Upd = 33,

        /// <summary>
        /// Воврат розничной продажи
        /// </summary>
        RetailRefund = 34,
        /// <summary>
        /// Приход без документов
        /// </summary>
        ProductIncome = 35,

        /// <summary>
        /// Исходящий универсальный передаточный документ
        /// </summary>
        SalesUpd = 36,
        
        /// <summary>
        /// Отчет о выпуске готовой продукции
        /// </summary>
        ManufacturingReport = 37,

        /// <summary>
        /// Условный документ для налогового учета остатков по 71.01 в авансовом отчёте
        /// </summary>
        AccountingBalancePseudoDocument = 51,
        
        /// <summary>
        /// Основной договор в учетке
        /// </summary>
        MainContract = 52,

        /// <summary>
        /// Транспортная накладная
        /// </summary>
        TransportWaybill = 53,

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
        PurchasesCurrencyInvoice = 56,
        
        /// <summary>
        /// УКД
        /// </summary>
        Ukd = 57,

        /// <summary>
        /// Розничная выручка или БСО (Z-отчет)
        /// </summary>
        RetailRevenue = 58,

        /// <summary>
        /// Отчет комиссионера
        /// </summary>
        CommissionAgentReport = 59,

        /// <summary>
        /// Резервный документ для платежей
        /// Эмулирует привязанный подтверждающий документ
        /// </summary>
        PaymentReserve = 60,

        /// <summary>
        /// Дочерний бюджетный платеж для ЕНП
        /// </summary>
        UnifiedBudgetaryPayment = 61,

        /// <summary>
        /// Складская операция разукомплектации
        /// </summary>
        StockUnbundling = 62,

        /// <summary>
        /// Неизвестный нам подтверждающий документ
        /// </summary>
        Other = 101
    }
}