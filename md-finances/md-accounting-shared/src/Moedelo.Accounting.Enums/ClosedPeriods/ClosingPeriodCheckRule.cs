namespace Moedelo.Accounting.Enums.ClosedPeriods
{
    /// <summary>
    /// Проверка при закрытии перида.
    /// https://confluence.mdtest.org/pages/viewpage.action?pageId=12948971
    /// </summary>
    /// <remarks>
    /// Важно! Правило может трактоваться по-разному в записимости от СНО и ОПФ: ошибка/предупреждение/игнорируется.
    /// Рекомендуется придерживаться правила: отрицательные значения могут быть ошибками, положительные - только предупреждениями. 
    /// </remarks>
    public enum ClosingPeriodCheckRule
    {
        /// <summary>
        /// Введены некорректные остатки
        /// </summary>
        IncorrectAccountingBalances = -1,

        // HasNotProvidedUpd = -2, объединено с -10

        /// <summary>
        /// Отрицательное количество товаров/материалов на складе
        /// </summary>
        StockNegativeBalances = -3,

        // HasNotProvidedRetailRefund = -4,  объединено с -10

        /// <summary>
        /// Не начислен уставный капитал
        /// </summary>
        NoAuthorizedCapital = -5,
        
        // MustInputAuthorizedCapitalInRemains = -6, объединено с -5
        
        // MustInputAuthorizedCapitalByAccountingStatement = -7, объединено с -5

        /// <summary>
        /// Материалы с типом сырье (счет 10.01) в документах комплектации
        /// </summary>
        SoldRawMaterialsUsedInBundling = -8,

        /// <summary>
        /// При производстве готовой продукции использовано больше сырья, чем выписано со склада
        /// </summary>
        ManufacturingMaterialsNegativeBalances = -9,

        /// <summary>
        /// Неучтенные в бухгалтерском учете документы
        /// </summary>
        NotProvidedDocuments = -10,
        
        /// <summary>
        /// Не начислен НДС в бюджет
        /// </summary>
        HasNdsForAccrual = -11,

        /// <summary>
        /// Временно заблокирован/ошибка с кастомным описанием
        /// </summary>
        TemporaryBlocked = -12,

        /// <summary>
        /// Приходы без документов, в которых используются комплекты
        /// </summary>
        DebitWithoutDocsWithBundling = -13,

        /// <summary>
        /// Нужно закрыть УСН декларацию перед началом действия ФИФО
        /// </summary>
        NeedCloseUsnDeclarationBeforeFifo = -14,
        
        /// <summary>
        /// Выставлены счета-фактуры (но не должны)
        /// </summary>
        Invoices = 1,
        
        /// <summary>
        /// Оплаты не связаны с подтверждающими документами
        /// </summary>
        UncoveredPayments = 2,
        
        /// <summary>
        /// Розничные возвраты не связаны с платежами
        /// </summary>
        NotPaidRetailRefunds = 3,

        /// <summary>
        /// Отрицательные БУ-остатки (из реквизитов)
        /// </summary>
        NegativeAccBalancesInPreviousYear = 4,
        
        /// <summary>
        /// Отрицательные остатки в БУ на момент закрытия периода
        /// </summary>
        NegativeAccBalances = 5,
        
        /// <summary>
        /// Перечисление аванса по договору аренды (лизинга)
        /// </summary>
        ReceiptStatementsWithoutAdvance = 6,
        
        /// <summary>
        /// УКД без связи с операцией списания «Возврат покупателю»
        /// </summary>
        UkdWithoutRefundPayments = 7,
        
        /// <summary>
        /// Доступный к вычету НДС
        /// </summary>
        HasNdsForDeduction = 8,
    }
}