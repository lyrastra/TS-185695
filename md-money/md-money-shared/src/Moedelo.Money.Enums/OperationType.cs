using Moedelo.Money.Enums.Attributes;
using System.ComponentModel;

namespace Moedelo.Money.Enums
{
    /// <summary>
    /// ВАЖНО: Все новые операции нужно синхронизировать с 
    /// https://github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Enums/PostingEngine/OperationType.cs
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Входящее П/П: Поступление с другого счета
        /// </summary>
        [Description("Перевод со счета")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingTransferFromAccount = 10,

        /// <summary>
        /// Входящее П/П: Прочие поступления
        /// </summary>
        [Description("Прочее")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingOther = 14,

        /// <summary>
        /// Входящее П/П: Поступление в оплату продажи товаров/материалов/работ/услуг
        /// </summary>
        [Description("Оплата от покупателя")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingPaymentFromCustomer = 16,

        /// <summary>
        /// Входящее П/П: Возврат от подотчетного лица
        /// </summary>
        [Description("Возврат от подотчетного лица")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingRefundFromAccountablePerson = 18,

        /// <summary>
        /// Исходящее П/П: Возврат покупателю
        /// </summary>
        [Description("Возврат покупателю")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingRefundToCustomer = 22,

        /// <summary>
        /// Исходящее П/П: Выдача подотчетному лицу
        /// </summary>
        [Description("Выдача подотчетному лицу")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingPaymentToAccountablePerson = 23,

        /// <summary>
        /// Исходящее П/П: Прочий платеж
        /// </summary>
        [Description("Прочее")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingOther = 24,

        /// <summary>
        /// Исходящее П/П: Платеж поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Оплата поставщику")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingPaymentToSupplier = 26,

        /// <summary>
        /// Исходящее П/П: Перечисление в связи с оплатой труда
        /// </summary>
        [Description("Выплаты физ. лицам")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingPaymentToNaturalPersons = 28,

        /// <summary>
        /// Исходящее П/П: Перевод на другой счет
        /// </summary>
        [Description("Перевод на другой счет")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingTransferToAccount = 29,

        /// <summary>
        /// Мемориальный ордер: Списана комиссия банка
        /// </summary>
        [Description("Списана комиссия банка")]
        [OperationKind(OperationKind.PaymentOrder)]
        MemorialWarrantBankFee = 30,

        /// <summary>
        /// Мемориальный ордер: Зачисление инкассированных денежных средств
        /// </summary>
        [Description("Инкассированные денежные средства")]
        [OperationKind(OperationKind.PaymentOrder)]
        MemorialWarrantTransferFromCashCollection = 31,

        /// <summary>
        /// Мемориальный ордер: Поступление наличных денежных средств из кассы
        /// </summary>
        [Description("Поступление из кассы")]
        [OperationKind(OperationKind.PaymentOrder)]
        MemorialWarrantTransferFromCash = 32,

        /// <summary>
        /// Мемориальный ордер: Розничная выручка (Поступление за товар, оплаченный банковской картой)
        /// </summary>
        [Description("Эквайринг")]
        [OperationKind(OperationKind.PaymentOrder)]
        MemorialWarrantRetailRevenue = 33,

        /// <summary>
        /// Мемориальный ордер: Снятие с расчетного счета
        /// </summary>
        [Description("Снятие с расчетного счета")]
        [OperationKind(OperationKind.PaymentOrder)]
        MemorialWarrantWithdrawalFromAccount = 34,

        /// <summary>
        /// Бюджетный платеж
        /// </summary>
        [Description("Бюджетный платеж")]
        [OperationKind(OperationKind.PaymentOrder)]
        BudgetaryPayment = 39,

        [Description("Посредническое вознаграждение")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingMediationFee = 97,

        [Description("Поступление с электронного кошелька")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingTransferFromPurse = 103,

        [Description("Начисление процентов от банка")]
        [OperationKind(OperationKind.PaymentOrder)]
        MemorialWarrantAccrualOfInterest = 105,

        /// <summary>
        /// Входящее П/П: Получение займа
        /// </summary>
        [Description("Получение займа или кредита")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingLoanObtaining = 108,

        /// <summary>
        /// Исходящее П/П: Погашение займа
        /// </summary>
        [Description("Погашение займа или процентов")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingLoanRepayment = 109,

        /// <summary>
        /// Входящее П/П: Фин. помощь от учредителя
        /// </summary>
        [Description("Финансовая помощь от учредителя")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingFinancialAssistance = 112,

        /// <summary>
        /// Входящее П/П: Взнос в уставный капитал
        /// </summary>
        [Description("Взнос в уставный капитал")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingContributionToAuthorizedCapital = 114,

        /// <summary>
        /// Исходящее П/П: Выплата по агентскому договору
        /// </summary>
        [Description("Выплата по агентскому договору")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingAgencyContract = 116,

        /// <summary>
        /// Входящее П/П: Взнос собственных средств
        /// </summary>
        [Description("Взнос собственных средств")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingContributionOfOwnFunds = 118,

        /// <summary>
        /// Исходящее П/П: Снятие прибыли
        /// </summary>
        [Description("Снятие прибыли")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingWithdrawalOfProfit = 120,

        /// <summary>
        /// Исходящее П/П: Покупка валюты
        /// </summary>
        [Description("Покупка валюты")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingCurrencyPurchase = 130,

        /// <summary>
        /// Входящее П/П: Поступление от покупки валюты
        /// </summary>
        [Description("Поступление от покупки валюты")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingCurrencyPurchase = 131,

        /// <summary>
        /// Исходящее П/П: Продажа валюты
        /// </summary>
        [Description("Продажа валюты")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingCurrencySale = 132,

        /// <summary>
        /// Входящее П/П: Поступление от продажи валюты
        /// </summary>
        [Description("Поступление от продажи валюты")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingCurrencySale = 133,

        /// <summary>
        /// Входящее П/П: Прочее валютное поступление
        /// </summary>
        [Description("Прочее")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingCurrencyOther = 134,

        /// <summary>
        /// Валютная комиссия банка
        /// </summary>
        [Description("Списания комиссия банка")]
        [OperationKind(OperationKind.PaymentOrder)]
        CurrencyBankFee = 135,

        /// <summary>
        /// Исходящее П/П: Валютный платеж поставщику
        /// </summary>
        [Description("Оплата поставщику")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingCurrencyPaymentToSupplier = 136,

        /// <summary>
        /// Исходящее П/П: Прочее валютное списание
        /// </summary>
        [Description("Прочее")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingCurrencyOther = 137,

        /// <summary>
        /// Входящее П/П: Валютная оплата от покупателя
        /// </summary>
        [Description("Оплата от покупателя")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingCurrencyPaymentFromCustomer = 138,

        /// <summary>
        /// Исходящее П/П: Перевод на другой валютный счет
        /// </summary>
        [Description("Перевод на другой счет")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingCurrencyTransferToAccount = 139,

        /// <summary>
        /// Входящее П/П: Поступление с другого валютного счета
        /// </summary>
        [Description("Перевод со счета")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingCurrencyTransferFromAccount = 140,

        /// <summary>
        /// Исходящее П/П: Арендный платеж
        /// </summary>
        [Description("Арендный платеж")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingRentPayment = 141,

        /// <summary>
        /// Исходящее П/П: Выдача займа
        /// </summary>
        [Description("Выдача займа")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingLoanIssue = 142,

        /// <summary>
        /// Входящее П/П: Возврат займа или процентов
        /// </summary>
        [Description("Возврат займа или процентов")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingLoanReturn = 143,

        /// <summary>
        /// Входящее П/П: Поступление от комиссионера
        /// </summary>
        [Description("Поступление от комиссионера")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingIncomeFromCommissionAgent = 154,

        /// <summary>
        /// Исходящее П/П: Выплаты удержаний
        /// </summary>
        [Description("Выплаты удержаний")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingDeduction = 156,

        /// <summary>
        /// Входящее П/П: Возврат на расчётный счёт
        /// </summary>
        [Description("Возврат на расчётный счёт")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderIncomingRefundToSettlementAccount = 157,

        /// <summary>
        /// Исходящее П/П: Единый налоговый платеж (aka Бюджетный платеж)
        /// </summary>
        [Description("Единый налоговый платеж")]
        [OperationKind(OperationKind.PaymentOrder)]
        PaymentOrderOutgoingUnifiedBudgetaryPayment = 158,


        // Операции по кассе


        /// <summary>
        /// Приходный кассовый ордер: Перемещение с другой кассы
        /// </summary>
        [Description("Перемещение из другой кассы")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingFromAnotherCash = 45,

        /// <summary>
        /// Приходный кассовый ордер: Снятие с расчетного счета
        /// </summary>
        [Description("Поступление с расчётного счёта")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingFromSettlementAccount = 46,

        /// <summary>
        /// Приходный кассовый ордер: Розничная выручка
        /// </summary>
        [Description("Розничная выручка")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingFromRetailRevenue = 47,

        /// <summary>
        /// Приходный кассовый ордер: Поступление в оплату продажи товаров/материалов/работ/услуг
        /// </summary>
        [Description("Оплата от покупателя")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingPaymentForGoods = 49,

        /// <summary>
        /// Приходный кассовый ордер: Возврат от подотчетного лица
        /// </summary>
        [Description("Возврат от подотчетного лица")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingReturnFromAccountablePerson = 53,

        /// <summary>
        /// Приходный кассовый ордер: Прочие поступления
        /// </summary>
        [Description("Прочее")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingOther = 54,

        /// <summary>
        /// Расходный кассовый ордер: Зачисление на расчетный счет
        /// </summary>
        [Description("Зачисление на расчетный счет")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutcomingToSettlementAccount = 55,

        /// <summary>
        ///  Расходный кассовый ордер: Возврат покупателю
        /// </summary>
        [Description("Возврат покупателю")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingReturnToBuyer = 58,

        /// <summary>
        ///  Расходный кассовый ордер: Выдача подотчетному лицу
        /// </summary>
        [Description("Выдача подотчетному лицу")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingIssuanceAccountablePerson = 59,

        /// <summary>
        ///  Расходный кассовый ордер: Прочий платеж
        /// </summary>
        [Description("Прочее")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingOther = 60,

        /// <summary>
        ///  Расходный кассовый ордер: Платеж поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Оплата поставщику")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingPaymentSuppliersForGoods = 62,

        /// <summary>
        ///  Расходный кассовый ордер: Выплата в связи с оплатой труда
        /// </summary>
        [Description("Выплаты физ.лицам")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingPaymentForWorking = 63,

        /// <summary>
        ///  Расходный кассовый ордер: Инкассация денег
        /// </summary>
        [Description("Инкассация денег")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingCollectionOfMoney = 64,

        /// <summary>
        /// Расходный кассовый ордер: Перевод в другую кассу
        /// </summary>
        [Description("Перевод в другую кассу")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingTranslatedToOtherCash = 65,

        [Description("Посредническое вознаграждение")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingMediationFee = 98,

        /// <summary>
        ///  Входящий РКО: Получение займа
        /// </summary>
        [Description("Получение займа или кредита")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingLoanObtaining = 110,

        /// <summary>
        ///  Исходящий РКО: Погашение займа
        /// </summary>
        [Description("Погашение займа или процентов")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingLoanRepayment = 111,

        /// <summary>
        /// Входящее РКО: Фин. помощь от учредителя
        /// </summary>
        [Description("Финансовая помощь от учредителя")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingMaterialAid = 113,

        /// <summary>
        /// Входящее РКО: Взнос в уставный капитал
        /// </summary>
        [Description("Взнос в уставный капитал")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingContributionAuthorizedCapital = 115,

        /// <summary>
        /// Исходящее РКО: Выплата по агентскому договору
        /// </summary>
        [Description("Выплата по агентскому договору")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingPaymentAgencyContract = 117,

        /// <summary>
        /// Входящее ПКО: Взнос собственных средств
        /// </summary>
        [Description("Взнос собственных средств")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingContributionOfOwnFunds = 119,

        /// <summary>
        /// Исходящее РКО: Снятие прибыли
        /// </summary>
        [Description("Снятие прибыли")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingProfitWithdrawing = 121,

        /// <summary>
        /// Приходный кассовый ордер: Розничная выручка по посредническому договору
        /// </summary>
        [Description("Розничная выручка по посредническому договору")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderIncomingMiddlemanRetailRevenue = 123,

        /// <summary>
        /// Исходящий кассовый ордер: Бюджетный платеж
        /// </summary>
        [Description("Бюджетный платеж")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderBudgetaryPayment = 126,

        /// <summary>
        /// Расходный кассовый ордер: Единый налоговый платеж (aka Бюджетный платеж)
        /// </summary>
        [Description("Единый налоговый платеж")]
        [OperationKind(OperationKind.CashOrder)]
        CashOrderOutgoingUnifiedBudgetaryPayment = 159,



        // Операции по эл. кошелькам



        [Description("Перевод на р/с")]
        [OperationKind(OperationKind.PurseOperation)]
        PurseOperationTransferToSettlement = 99,

        [Description("Удержание комиссии")]
        [OperationKind(OperationKind.PurseOperation)]
        PurseOperationComission = 100,

        [Description("Прочее списание")]
        [OperationKind(OperationKind.PurseOperation)]
        PurseOperationOtherOutgoing = 101,

        [Description("Оплата от покупателя")]
        [OperationKind(OperationKind.PurseOperation)]
        PurseOperationIncomingPaymentFromCustomer = 102
    }
}
