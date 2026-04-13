using System.ComponentModel;

namespace Moedelo.AccPostings.Enums
{
    // значения должны совпадать с https://github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Enums/PostingEngine/OperationType.cs
    public enum OperationType
    {
        Default = 0,
        /// <summary>
        /// Оприходывание на складе
        /// </summary>
        StockIncome = 1,

        /// <summary>
        /// Списание со склада
        /// </summary>
        StockOutgoing = 2,

        /// <summary>
        /// Перемещение на складе
        /// </summary>
        StockMovement = 3,

        /// <summary>
        /// Покупка товара по накладной
        /// </summary>
        IncomingWaybill = 4,

        /// <summary>
        /// Продажа товара по накладной
        /// </summary>
        OutgoingWaybill = 5,

        /// <summary>
        /// Возврат поставщику
        /// </summary>
        ReturnToSupplier = 6,

        /// <summary>
        /// Бухгалтерская справка
        /// </summary>
        AccountingStatement = 7,

        /// <summary>
        /// Входящая счет-фактура
        /// </summary>
        IncomingInvoice = 8,

        /// <summary>
        /// Исходящая счет-фактура
        /// </summary>
        OutgoingInvoice = 9,

        /// <summary>
        /// Входящее П/П: Поступление с другого счета
        /// </summary>
        [Description("Перевод со счета")]
        PaymentOrderIncomingFromAnotherAccount = 10,

        /// <summary>
        /// Входящее П/П: Получена предоплата от покупателя объектов основных средств
        /// </summary>
        [Description("Получена предоплата от покупателя объектов основных средств")]
        PaymentOrderIncomingPrePaymentForFixedAssets = 11,

        /// <summary>
        /// Входящее П/П: Получена предоплата от покупателя товаров/материалов/работ/услуг
        /// </summary>
        [Description("Получена предоплата от покупателя товаров/материалов/работ/услуг")]
        PaymentOrderIncomingPrePaymentForGoods = 12,

        /// <summary>
        /// Входящее П/П: Возврат бюджетных платяжей
        /// </summary>
        [Description("Возврат бюджетных платяжей")]
        PaymentOrderIncomingReturnBudgetaryPayments = 13,

        /// <summary>
        /// Входящее П/П: Прочие поступления
        /// </summary>
        [Description("Прочее")]
        PaymentOrderIncomingOther = 14,

        /// <summary>
        /// Входящее П/П: Поступление в оплату продажи объекта основных средств
        /// </summary>
        [Description("Поступление в оплату продажи объекта основных средств")]
        PaymentOrderIncomingPaymentForFixedAssets = 15,

        /// <summary>
        /// Входящее П/П: Поступление в оплату продажи товаров/материалов/работ/услуг
        /// </summary>
        [Description("Оплата от покупателя")]
        PaymentOrderIncomingPaymentForGoods = 16,

        /// <summary>
        /// Входящее П/П: Поступление суммы от внебюджетных фондов
        /// </summary>
        [Description("Поступление суммы от внебюджетных фондов")]
        PaymentOrderIncomingFromNonBudgetaryFunds = 17,

        /// <summary>
        /// Входящее П/П: Возврат от подотчетного лица
        /// </summary>
        [Description("Возврат от подотчетного лица")]
        PaymentOrderIncomingReturnFromAccountablePerson = 18,

        /// <summary>
        /// Входящее П/П: Возврат от поставщика
        /// </summary>
        [Description("Возврат от поставщика")]
        PaymentOrderIncomingReturnFromSupplier = 19,

        /// <summary>
        /// Исходящее П/П: Аванс поставщику за основные средства
        /// </summary>
        [Description("Аванс поставщику за основные средства")]
        PaymentOrderOutgoingPrePaymentSupplierForFixedAssets = 20,

        /// <summary>
        /// Исходящее П/П: Аванс поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Аванс поставщику за товар/материал/работу/услуги")]
        PaymentOrderOutgoingPrePaymentSupplierForGoods = 21,

        /// <summary>
        /// Исходящее П/П: Возврат покупателю
        /// </summary>
        [Description("Возврат покупателю")]
        PaymentOrderOutgoingReturnToBuyer = 22,

        /// <summary>
        /// Исходящее П/П: Выдача подотчетному лицу
        /// </summary>
        [Description("Выдача подотчетному лицу")]
        PaymentOrderOutgoingPaymentToAccountablePerson = 23,

        /// <summary>
        /// Исходящее П/П: Прочий платеж
        /// </summary>
        [Description("Прочее")]
        PaymentOrderOutgoingOther = 24,

        /// <summary>
        /// Исходящее П/П: Платеж поставщику за основные средства
        /// </summary>
        [Description("Платеж поставщику за основные средства")]
        PaymentOrderOutgoingPaymentSuppliersForFixedAssets = 25,

        /// <summary>
        /// Исходящее П/П: Платеж поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Оплата поставщику")]
        PaymentOrderOutgoingPaymentSuppliersForGoods = 26,

        /// <summary>
        /// Исходящее П/П: Перечисление в связи с оплатой труда
        /// </summary>
        [Description("Выплаты физ. лицам")]
        PaymentOrderOutgoingPaymentToNaturalPersons = 28,

        /// <summary>
        /// Исходящее П/П: Перевод на другой счет
        /// </summary>
        [Description("Перевод на другой счет")]
        PaymentOrderOutgoingTransferToOtherAccount = 29,

        /// <summary>
        /// Мемориальный ордер: Списана комиссия банка
        /// </summary>
        [Description("Списана комиссия банка")]
        MemorialWarrantBankFeeIsDeducted = 30,

        /// <summary>
        /// Мемориальный ордер: Зачисление инкассированных денежных средств
        /// </summary>
        [Description("Инкассированные денежные средства")]
        MemorialWarrantCreditingCollectedFunds = 31,

        /// <summary>
        /// Мемориальный ордер: Поступление наличных денежных средств из кассы
        /// </summary>
        [Description("Поступление из кассы")]
        MemorialWarrantReceiptFromCash = 32,

        /// <summary>
        /// Мемориальный ордер: Поступление за товар оплаченный банковской картой
        /// </summary>
        [Description("Поступление за товар оплаченный банковской картой")]
        MemorialWarrantReceiptGoodsPaidCreditCard = 33,

        /// <summary>
        /// Мемориальный ордер: Снятие с расчетного счета
        /// </summary>
        [Description("Снятие с расчетного счета")]
        MemorialWarrantWithdrawalFromAccount = 34,

        /// <summary>
        /// Исходящая накладная на безвозмездное получение
        /// </summary>
        OutgoingWaybillDonation = 35,

        /// <summary>
        /// Входящий акт
        /// </summary>
        IncomingStatement = 36,

        /// <summary>
        /// Ввод остатков
        /// </summary>
        AccountingBalance = 37,

        /// <summary>
        /// Возврат от покупателя
        /// </summary>
        ReturnFromBuyer = 38,

        /// <summary>
        /// Бюджетный платеж
        /// </summary>
        [Description("Бюджетный платеж")]
        BudgetaryPayment = 39,

        /// <summary>
        /// Входящий акт приёма-передачи ОС
        /// </summary>
        IncomingStatementOfFixedAsset = 40,

        /// <summary>
        /// Инвентарная карточка
        /// </summary>
        InventoryCard = 41,

        ClosingExpensesAccounts = 42,

        /// <summary>
        /// Финансовый результат
        /// </summary>
        FinancialResult = 43,

        /// <summary>
        /// Амортизация основного средства
        /// </summary>
        Amortization = 44,

        /// <summary>
        /// Приходный кассовый ордер: Перемещение с другой кассы
        /// </summary>
        [Description("Перемещение из другой кассы")]
        CashOrderIncomingFromAnotherCash = 45,

        /// <summary>
        /// Приходный кассовый ордер: Снятие с расчетного счета
        /// </summary>
        [Description("Поступление с расчётного счёта")]
        CashOrderIncomingFromSettlementAccount = 46,

        /// <summary>
        /// Приходный кассовый ордер: Розничная выручка
        /// </summary>
        [Description("Розничная выручка")]
        CashOrderIncomingFromRetailRevenue = 47,

        /// <summary>
        /// Приходный кассовый ордер: Поступление в оплату продажи объекта основных средств
        /// </summary>
        [Description("Поступление в оплату продажи объекта основных средств")]
        CashOrderIncomingPaymentForFixedAssets = 48,

        /// <summary>
        /// Приходный кассовый ордер: Поступление в оплату продажи товаров/материалов/работ/услуг
        /// </summary>
        [Description("Оплата от покупателя в кассе")]
        CashOrderIncomingPaymentForGoods = 49,

        /// <summary>
        /// Приходный кассовый ордер: Получена предоплата от покупателя основных средств
        /// </summary>
        [Description("Получена предоплата от покупателя основных средств")]
        CashOrderIncomingPrePaymentForFixedAssets = 50,

        /// <summary>
        /// Приходный кассовый ордер: Получена предоплата от покупателя товаров/материалов/работ/услуг
        /// </summary>
        [Description("Получена предоплата от покупателя товаров/материалов/работ/услуг")]
        CashOrderIncomingPrePaymentForGoods = 51,

        /// <summary>
        /// Приходный кассовый ордер: Возврат от поставщика
        /// </summary>
        [Description("Возврат от поставщика")]
        CashOrderIncomingReturnFromSupplier = 52,

        /// <summary>
        /// Приходный кассовый ордер: Возврат от подотчетного лица
        /// </summary>
        [Description("Возврат от подотчетного лица")]
        CashOrderIncomingReturnFromAccountablePerson = 53,

        /// <summary>
        /// Приходный кассовый ордер: Прочие поступления
        /// </summary>
        [Description("Прочие поступления")]
        CashOrderIncomingOther = 54,

        /// <summary>
        /// Расходный кассовый ордер: Зачисление на расчетный счет
        /// </summary>
        [Description("Зачисление на расчетный счет")]
        CashOrderOutcomingToSettlementAccount = 55,

        /// <summary>
        ///  Расходный кассовый ордер: Аванс поставщику за основные средства
        /// </summary>
        [Description("Аванс поставщику за основные средства")]
        CashOrderOutgoingPrePaymentBySupplierForFixedAssets = 56,

        /// <summary>
        ///  Расходный кассовый ордер: Аванс поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Аванс поставщику за товар/материал/работу/услуги")]
        CashOrderOutgoingPrePaymentSupplierForGoods = 57,

        /// <summary>
        ///  Расходный кассовый ордер: Возврат покупателю
        /// </summary>
        [Description("Возврат покупателю")]
        CashOrderOutgoingReturnToBuyer = 58,

        /// <summary>
        ///  Расходный кассовый ордер: Выдача подотчетному лицу
        /// </summary>
        [Description("Выдача подотчетному лицу")]
        CashOrderOutgoingIssuanceAccountablePerson = 59,

        /// <summary>
        ///  Расходный кассовый ордер: Прочий платеж
        /// </summary>
        [Description("Прочий платеж")]
        CashOrderOutgoingOther = 60,

        /// <summary>
        ///  Расходный кассовый ордер: Платеж поставщику за основные средства
        /// </summary>
        [Description("Платеж поставщику за основные средства")]
        CashOrderOutgoingPaymentSuppliersForFixedAssets = 61,

        /// <summary>
        ///  Расходный кассовый ордер: Платеж поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Оплата поставщику в кассе")]
        CashOrderOutgoingPaymentSuppliersForGoods = 62,

        /// <summary>
        ///  Расходный кассовый ордер: Выплата в связи с оплатой труда
        /// </summary>
        [Description("Выплата в связи с оплатой труда")]
        CashOrderOutgoingPaymentForWorking = 63,

        /// <summary>
        ///  Расходный кассовый ордер: Инкассация денег
        /// </summary>
        [Description("Инкассация денег")]
        CashOrderOutgoingCollectionOfMoney = 64,

        /// <summary>
        /// Расходный кассовый ордер: Перевод в другую кассу
        /// </summary>
        [Description("Перевод в другую кассу")]
        CashOrderOutgoingTranslatedToOtherCash = 65,

        /// <summary>
        /// Авансовый отчет - командировочные расходы
        /// </summary>
        [Description("Командировочные расходы")]
        AdvanceStatementBusinessTrip = 66,

        /// <summary>
        /// Авансовый отчет - оплата поставщику
        /// </summary>
        [Description("Оплата поставщику")]
        AdvanceStatementPaymentToSupplier = 67,

        /// <summary>
        /// Авансовый отчет - хозяйственные расходы
        /// </summary>
        [Description("Хозяйственные расходы")]
        AdvanceStatementAdministrativeExpenses = 68,

        /// <summary>
        /// Авансовый отчет - продукты и материалы
        /// </summary>
        [Description("Продукты и материалы")]
        AdvanceStatementProductAndMaterial = 69,

        /// <summary>
        /// Авансовый отчет - услуги
        /// </summary>
        [Description("Услуги")]
        AdvanceStatementService = 70,

        [Description("Начисление оклада персоналу")]
        SalarySalaryScale = 71,

        [Description("Начисление больничных персоналу")]
        SalaryChargeSickList = 72,

        [Description("Начисление перемий персоналу")]
        SalaryChargeProductionPremium = 73,

        [Description("Начисление командировочных персоналу")]
        SalaryChargeBusinessTrip = 74,

        [Description("Начисление по ГПД")]
        SalaryChargeByGpd = 75,

        [Description("Начисление отпускных персоналу")]
        SalaryChargeVacation = 76,

        [Description("Начисление выплат при увольнении")]
        SalaryChargeByFiredWorker = 77,

        [Description("Начиление материальной помощи и выплаты социального характера")]
        SalaryChargeMaterialAid = 78,

        [Description("Начисление работникам доходов от участия в капитале организации")]
        SalaryChargeNdfl = 79,

        [Description("Начислены страховые взносы на обязательное социальное страхование в ФСС на выплаты персоналу")]
        SalaryDisabilityFss = 80,

        [Description("Начислены страховые взносы на обязательное социальное страхование от несчастных случаев на производстве и профессиональных заболеваний на выплаты  персоналу")]
        SalaryInjuredFss = 81,

        [Description("Начислены страховые взносы на  обязательное пенсионное страхование в ПФР (страховая часть трудовой пенсии) на выплаты  персоналу")]
        SalaryInsurancePfr = 82,

        [Description("Начислены страховые взносы на  обязательное пенсионное страхование в ПФР (накопительная часть трудовой пенсии) на выплаты  персоналу")]
        SalaryAccumulatePfr = 83,

        [Description("Начислены страховые взносы на  обязательное медицинское страхование в ФОМС на выплаты  персоналу")]
        SalaryFederalFoms = 84,

        /// <summary> CustomCharge </summary>
        [Description("Начисление прочих доходов")]
        SalaryCustomCharge = 85,

        /// <summary>
        /// Начисление налога на прибыль
        /// </summary>
        ProfitTax = 86,

        /// <summary>
        /// Реформация баланса
        /// </summary>
        BalanceReformation = 87,

        /// <summary> Формирование УК </summary>
        [Description("Формирование уставного капитал")]
        AuthorizedCapital = 88,

        /// <summary>
        /// Закрытие года
        /// </summary>
        ClosingYear = 89,

        /// <summary>
        /// Недостача по инвентаризации
        /// </summary>
        InventoryLoss = 90,

        /// <summary>
        /// Избыток по инвентаризации
        /// </summary>
        InventoryExcess = 91,

        SelfCostInventory = 92,

        /// <summary>
        /// Торговая наценка
        /// </summary>
        RetailMargin = 93,

        /// <summary>
        /// Торговая наценка (СТОРНО)
        /// </summary>
        RetailMarginStorno = 94,

        /// <summary>
        /// Начисление налога на имущество.
        /// </summary>
        EstateTax = 95,

        /// <summary>
        /// Договор о посредничестве
        /// </summary>
        [Description("Посредническое вознаграждение")]
        MiddlemanContract = 96,

        [Description("Посредническое вознаграждение")]
        PaymentOrderIncomingMediationFee = 97,

        [Description("Посредническое вознаграждение")]
        CashOrderIncomingMediationFee = 98,

        [Description("Перевод на р/с")]
        PurseOperationTransferToSettlement = 99,

        [Description("Удержание комиссии")]
        PurseOperationComission = 100,

        [Description("Прочее списание")]
        PurseOperationOtherOutgoing = 101,

        [Description("Поступление")]
        PurseOperationIncome = 102,

        [Description("Поступление с электронного кошелька")]
        PaymentOrderIncomingFromPurse = 103,

        [Description("Начислены страховые взносы на  обязательное пенсионное страхование в ПФР (страховая часть трудовой пенсии c превышения базы) на выплаты  персоналу")]
        SalaryInsuranceSolidaryPartPfr = 104,

        [Description("Начисление процентов от банка")]
        MemorialWarrantAccrualOfInterest = 105,

        /// <summary>
        /// Комплектация на складе
        /// </summary>
        StockBundling = 106,

        /// <summary>
        /// Входящий УПД
        /// </summary>
        IncomingUpd = 107,

        /// <summary>
        /// Входящее П/П: Получение займа
        /// </summary>
        [Description("Получение займа")]
        PaymentOrderIncomingLoanObtaining = 108,

        /// <summary>
        /// Исходящее П/П: Погашение займа
        /// </summary>
        [Description("Погашение займа")]
        PaymentOrderOutgoingLoanRepayment = 109,

        /// <summary>
        ///  Входящий РКО: Получение займа
        /// </summary>
        [Description("Получение займа")]
        CashOrderIncomingLoanObtaining = 110,

        /// <summary>
        ///  Исходящий РКО: Погашение займа
        /// </summary>
        [Description("Погашение займа")]
        CashOrderOutgoingLoanRepayment = 111,

        /// <summary>
        /// Входящее П/П: Фин. помощь от учредителя
        /// </summary>
        [Description("Финансовая помощь от учредителя")]
        PaymentOrderIncomingMaterialAid = 112,

        /// <summary>
        /// Входящее РКО: Фин. помощь от учредителя
        /// </summary>
        [Description("Финансовая помощь от учредителя")]
        CashOrderIncomingMaterialAid = 113,

        /// <summary>
        /// Входящее П/П: Взнос в уставный капитал
        /// </summary>
        [Description("Взнос в уставный капитал")]
        PaymentOrderIncomingContributionAuthorizedCapital = 114,

        /// <summary>
        /// Входящее РКО: Взнос в уставный капитал
        /// </summary>
        [Description("Взнос в уставный капитал")]
        CashOrderIncomingContributionAuthorizedCapital = 115,

        /// <summary>
        /// Исходящее П/П: Выплата по агентскому договору
        /// </summary>
        [Description("Выплата по агентскому договору")]
        PaymentOrderOutgoingPaymentAgencyContract = 116,

        /// <summary>
        /// Исходящее РКО: Выплата по агентскому договору
        /// </summary>
        [Description("Выплата по агентскому договору")]
        CashOrderOutgoingPaymentAgencyContract = 117,

        /// <summary>
        /// Входящее П/П: Взнос собственных средств
        /// </summary>
        [Description("Взнос собственных средств")]
        PaymentOrderIncomingContributionOfOwnFunds = 118,

        /// <summary>
        /// Входящее ПКО: Взнос собственных средств
        /// </summary>
        [Description("Взнос собственных средств")]
        CashOrderIncomingContributionOfOwnFunds = 119,

        /// <summary>
        /// Исходящее П/П: Снятие прибыли
        /// </summary>
        [Description("Снятие прибыли")]
        PaymentOrderOutgoingProfitWithdrawing = 120,

        /// <summary>
        /// Исходящее РКО: Снятие прибыли
        /// </summary>
        [Description("Снятие прибыли")]
        CashOrderOutgoingProfitWithdrawing = 121,

        /// <summary>
        /// Исходящий УПД
        /// </summary>
        OutgoingUpd = 122,

        /// <summary>
        /// Приходный кассовый ордер: Розничная выручка по посредническому договору
        /// </summary>
        [Description("Розничная выручка по посредническому договору")]
        CashOrderIncomingMiddlemanRetailRevenue = 123,

        /// <summary>
        /// Отчет о выпуске готовой продукции
        /// </summary>
        Manufacturing = 124,

        /// <summary>
        /// Баланс по р/сч
        /// </summary>
        [Description("Баланс по р/сч")]
        PaymentOrderIncomingBalance = 125,

        /// <summary>
        /// Исходящее П/П: Арендный платеж
        /// </summary>
        [Description("Арендный платеж")]
        PaymentOrderOutgoingRentPayment = 126,

        /// <summary>
        /// Входящее П/П: Возврат займа или процентов
        /// </summary>
        [Description("Возврат займа или процентов")]
        PaymentOrderIncomingLoanReturn = 127,

        /// <summary>
        /// Исходящее П/П: Выдача займа
        /// </summary>
        [Description("Выдача займа")]
        PaymentOrderOutgoingLoanIssue = 128,

        /// <summary>
        /// Входящее П/П: Поступление от покупки валюты
        /// </summary>
        [Description("Поступление от покупки валюты")]
        PaymentOrderIncomingPurchaseCurrency = 131,

        /// <summary>
        /// Исходящее П/П: Покупка иностранной валюты
        /// </summary>
        [Description("Покупка иностранной валюты")]
        PaymentOrderOutgoingCurrencyPurchase = 130,

        /// <summary>
        /// Исходящее П/П: Продажа валюты
        /// </summary>
        [Description("Продажа валюты")]
        PaymentOrderOutgoingCurrencySale = 132,

        /// <summary>
        /// Входящее П/П: Поступление от продажи валюты
        /// </summary>
        [Description("Поступление от продажи валюты")]
        PaymentOrderIncomingSaleCurrency = 133,

        /// <summary>
        /// Входящее П/П: Прочее валютное поступление
        /// </summary>
        [Description("Прочее")]
        PaymentOrderIncomingCurrencyOther = 134,

        /// <summary>
        /// Исходящее П/П: Списана комиссия банка валютная операция
        /// </summary>
        [Description("Списана комиссия банка")]
        PaymentOrderOutgoingCurrencyBankFee = 135,

        // --- Внимание! --- этот enum расходится c (со значения 126) https://github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Enums/PostingEngine/OperationType.cs

        /// <summary>
        /// Исходящее П/П: Валютный платеж поставщику
        /// </summary>
        [Description("Оплата поставщику")]
        PaymentOrderOutgoingCurrencyPaymentToSupplier = 136,

        /// <summary>
        /// Исходящее П/П: Прочее валютное списание
        /// </summary>
        [Description("Прочее")]
        PaymentOrderOutgoingCurrencyOther = 137,

        /// <summary>
        /// Входящее П/П: Оплата от покупателя
        /// </summary>
        [Description("Оплата от покупателя")]
        PaymentOrderIncomingCurrencyPaymentFromCustomer = 138,

        /// <summary>
        /// Исходящее П/П: Перевод на другой счет (валютный)
        /// </summary>
        [Description("Перевод на другой счет")]
        PaymentOrderOutgoingCurrencyTransferToAccount = 139,

        /// <summary>
        /// Входящее П/П: Перевод со счета (валютный)
        /// </summary>
        [Description("Перевод со счета")]
        PaymentOrderIncomingCurrencyTransferFromAccount = 140,

        /// <summary>
        /// Исходящий инвойс (валюта)
        /// </summary>
        OutgoingCurrencyInvoice = 150,

        /// <summary>
        /// Входящий инвойс (валюта)
        /// </summary>
        IncomingCurrencyInvoice = 151,

        /// <summary>
        /// Продажи-УКД
        /// </summary>
        OutgoingUkd = 152,

        /// <summary>
        /// Курсовая разница (в инвойсе на продажу)
        /// </summary>
        ExchangeDifferenceInOutgoingCurrencyInvoice = 153,

        /// <summary>
        /// Поступление от комиссионера
        /// </summary>
        PaymentOrderIncomingIncomeFromCommissionAgent = 154,

        /// <summary>
        /// Отчет комиссионера
        /// </summary>
        CommissionAgentReport = 155,
        
        /// <summary>
        /// Исходящие П/П: Выплаты удержаний
        /// </summary>
        [Description("Выплаты удержаний")]
        PaymentOrderOutgoingDeduction = 156,

        /// <summary>
        /// Входящее П/П: Возврат на расчётный счёт
        /// </summary>
        PaymentOrderIncomingRefundToSettlementAccount = 157,

        /// <summary>
        /// Исходящее П/П: Единый налоговый платеж (aka Бюджетный платеж)
        /// </summary>
        [Description("Единый налоговый платеж")]
        PaymentOrderOutgoingUnifiedBudgetaryPayment = 158,
    }
}