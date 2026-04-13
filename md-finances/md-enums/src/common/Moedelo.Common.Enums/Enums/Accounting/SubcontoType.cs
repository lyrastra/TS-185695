using System.ComponentModel;
using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.Accounting
{
    public enum SubcontoType
    {
        None = 0,

        /// <summary> Должен быть привязан к контрагенту </summary>
        [Description("Контрагенты")]
        [MustBeAttachedToEntity]
        Kontragent = 1,

        /// <summary> Должен быть привязан к сотруднику </summary>
        [Description("Сотрудники")]
        [MustBeAttachedToEntity]
        Worker = 2,

        /// <summary> Должен быть привязан к договору </summary>
        [Description("Договоры")]
        [MustBeAttachedToEntity]
        Contract = 3,

        /// <summary> Должен быть привязан к банковскому счёту </summary>
        [Description("Банковские счета")]
        [MustBeAttachedToEntity]
        SettlementAccount = 4,

        [Description("Номенклатура")]
        [MustBeAttachedToEntity]
        Good = 5,

        /// <summary> Должен быть привязан к складу </summary>
        [Description("Склады")]
        [MustBeAttachedToEntity]
        Stock = 6,
 
        [Description("Ставки НДС")]
        [MustBeAttachedToEntity]
        NdsRate = 7,
        
        /// <summary>
        /// Платеж в бюджет
        /// </summary>
        [Description("Виды платежей в бюджет (фонды, КБК)")]
        BudgetaryPayment = 8,

        /// <summary>
        /// Виды деятельности
        /// </summary>
        [Description("Виды деятельности")]
        [MustBeAttachedToEntity]
        NomenclatureGroup = 9,

        [Description("Основные средства")]
        [MustBeAttachedToEntity]
        BasicAsset = 10,

        [Description("Нематериальные активы")]
        IntangibleAssets = 12,

        [Description("Объекты приобретения, создания строительства")]
        FixedAssetInvestment = 13,
        
        /// <summary>
        /// Статьи затрат
        /// </summary>
        [Description("Статьи затрат")]
        [MustBeAttachedToEntity]
        CostItems = 14,

        /// <summary>
        /// Способы строительства
        /// </summary>
        [Description("Способы строительства")]
        BuildingMethod = 15,

        /// <summary>
        /// Способы выполнения
        /// </summary>
        [Description("Способы выполнения")]
        PerformMethod = 16,
        
        [Description("Виды активов и обязательств")]
        TypeOfActives = 17,

        /// <summary>
        /// Первичные документы
        /// Должен быть привязан к первичному документу
        /// </summary>
        [Description("Первичные документы")]
        [MustBeAttachedToEntity]
        ReasonDocument = 18,

        /// <summary>
        /// Счета-фактуры выданные
        /// </summary>
        [Description("Счета-фактуры выданные")]
        [MustBeAttachedToEntity]
        InvoicesIssued = 19,

        /// <summary>
        /// Статьи движения основных средств
        /// </summary>
        [Description("Статьи движения денежных средств")]
        MoneyFlow = 20,

        /// <summary>
        /// Ценные бумаги
        /// </summary>
        [Description("Ценные бумаги")]
        Securities = 21,

        [Description("Уровни бюджетов")]
        BudgetaryLevel = 22,

        /// <summary>
        /// Виды расчетов по средствам ФСС
        /// </summary>
        [Description("Виды расчетов по средствам ФСС")]
        CalculationWithFss = 23,

        /// <summary>
        /// Расходы будущих периодов
        /// </summary>
        [Description("Расходы будущих периодов")]
        FutureOutgoing = 24,

        /// <summary>
        /// Исполнительные документы
        /// </summary>
        [Description("Исполнительные документы")]
        EnforcementDocuments = 25,

        /// <summary>
        /// Обособленные подразделения
        /// </summary>
        [Description("Подразделения")]
        SeparateDivision = 26,

        [Description("Направления использования прибыли")]
        UseOfProfit = 27,

        [Description("Назначение целевых средств")]
        AppointmentOfTrustFunds = 28,

        [Description("Движения целевых средств")]
        MovementOfTrustFunds = 29,

        [Description("Прочие доходы и расходы")]
        [MustNotBeAttachedToFirm]
        OtherIncomeOrOutgo = 30,
        
        /// <summary>
        /// Оценочные обязательства
        /// </summary>
        [Description("Оценочные обязательства")]
        EstimatedLiability = 31,
        
        /// <summary>
        /// Доходы будущих периодов
        /// </summary>
        [Description("Доходы будущих периодов")]
        FutureIncomes = 32,
        
        /// <summary>
        /// "Прибыли и убытки"
        /// </summary>
        [Description("Прибыли и убытки")]
        ProfitOrLoss = 33,

        /// <summary>
        /// Комиссионеры
        /// </summary>
        [Description("Комиссионеры")]
        Сommissioners = 34,

        /// <summary>
        /// "Бланки строгой отчетности"
        /// </summary>
        [Description("Бланки строгой отчетности")]
        AccountingForms = 35,

        /// <summary>
        /// Расходы на НИОКР
        /// </summary>
        [Description("Расходы на НИОКР")]
        ResearchOutgoing = 36,

        /// <summary> Должен быть привязан к кассе </summary>
        [Description("Кассы")]
        [MustBeAttachedToEntity]
        Cashes = 37,

        [Description("КБК")]
        Kbk = 38,

        [Description("Индивидуальный предприниматель")]
        Ip = 39,

        [Description("Основные средства")]
        UnbalanceFixedAsset = 40,

        [Description("Номенклатура")]
        UnbalanceNomenclature = 41,

        /// <summary>
        /// Специальные расчетные счета
        /// </summary>
        [Description("Специальные счета")]
        SpecialSettlementAccount = 42,

        /// <summary>
        /// Недостача
        /// </summary>
        [Description("Номенклатура, ОС и др.")]
        Deficit = 43,

        [Description("Склады")]
        UnbalanceStock = 44,

        /// <summary>
        /// Группировка субконто одного уровня в аналитике.
        /// </summary>
        [Description("Документы")]
        Documents = 45,

        [Description("Сотрудники (исполнители)")]
        WorkerAndGpd = 46,

        // 47 занят в acc-posting-shared
        /// <summary>
        /// Специальные расчетные счета для Цифрового Рубля
        /// </summary>
        [Description("Специальные счета для ЦР")]
        SpecialSettlementAccountForDigitalRuble = 48,

        [Description("Ставки туристического налога")]
        TouristTaxRate = 49
    }
}