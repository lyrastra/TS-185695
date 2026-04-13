using System;
using System.ComponentModel;
using Moedelo.PaymentOrderImport.Enums.Attributes;

namespace Moedelo.PaymentOrderImport.Enums
{
    /// <summary>
    /// ВАЖНО: Все новые операции нужно синхронизировать с 
    /// https://github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Enums/PostingEngine/OperationType.cs
    /// </summary>
    public enum PaymentImportOperationType
    {
        /// <summary>
        /// Входящее П/П: Поступление с другого счета
        /// </summary>
        [Description("Перевод со счета")]
        [TransferOperation]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingTransferFromAccount = 10,

        /// <summary>
        /// Входящее П/П: Прочие поступления
        /// </summary>
        [Description("Прочее поступление")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingOther = 14,

        /// <summary>
        /// Входящее П/П: Поступление в оплату продажи товаров/материалов/работ/услуг
        /// </summary>
        [Description("Оплата от покупателя")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingPaymentFromCustomer = 16,

        /// <summary>
        /// Входящее П/П: Возврат от подотчетного лица
        /// </summary>
        [Description("Возврат от подотчетного лица")]
        [UserRule]
        [OperationLegal(LegalType.None)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingRefundFromAccountablePerson = 18,

        /// <summary>
        /// Исходящее П/П: Возврат покупателю
        /// </summary>
        [Description("Возврат покупателю")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingRefundToCustomer = 22,
        
        [Description("Выдача подотчетному лицу")]
        [UserRule]
        [OperationLegal(LegalType.None)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingPaymentToAccountablePerson = 23,

        /// <summary>
        /// Исходящее П/П: Прочий платеж
        /// </summary>
        [Description("Прочее списание")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingOther = 24,

        /// <summary>
        /// Исходящее П/П: Платеж поставщику за товар/материал/работу/услуги
        /// </summary>
        [Description("Оплата поставщику")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingPaymentToSupplier = 26,
        
        /// <summary>
        /// Исходящее П/П: Перечисление в связи с оплатой труда
        /// </summary>
        [Description("Выплаты физ. лицам")]
        [UserRule]
        [OperationLegal(LegalType.None)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingPaymentToNaturalPersons = 28,

        /// <summary>
        /// Исходящее П/П: Перевод на другой счет
        /// </summary>
        [Description("Перевод на другой счет")]
        [UserRule]
        [TransferOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingTransferToAccount = 29,

        /// <summary>
        /// Списана комиссия банка
        /// </summary>
        [Description("Списана комиссия банка")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingBankFee = 30,

        /// <summary>
        /// Мемориальный ордер: Поступление наличных денежных средств из кассы
        /// </summary>
        [Description("Поступление из кассы")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingTransferFromCash = 32,

        /// <summary>
        /// Мемориальный ордер: Эквайринг (Розничная выручка/Поступление за товар оплаченный банковской картой)
        /// </summary>
        [Description("Эквайринг")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingRetailRevenue = 33,

        /// <summary>
        /// Мемориальный ордер: Снятие с расчетного счета
        /// </summary>
        [Description("Снятие с расчетного счета")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingtWithdrawalFromAccount = 34,

        /// <summary>
        /// Бюджетный платеж
        /// </summary>
        [Description("Бюджетный платеж")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        BudgetaryPayment = 39,

        [Description("Посредническое вознаграждение")]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingMediationFee = 97,

        [Description("Поступление с электронного кошелька")]
        [UserRule]
        [PurseOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingTransferFromPurse = 103,

        [Description("Начисление процентов от банка")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingAccrualOfInterest = 105,

        /// <summary>
        /// Входящее П/П: Получение займа
        /// </summary>
        [Description("Получение займа или кредита")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingLoanObtaining = 108,

        /// <summary>
        /// Исходящее П/П: Погашение займа
        /// </summary>
        [Description("Погашение займа или процентов")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingLoanRepayment = 109,

        /// <summary>
        /// Входящее П/П: Фин. помощь от учредителя
        /// </summary>
        [Description("Финансовая помощь от учредителя")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.Ooo)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingFinancialAssistance = 112,

        /// <summary>
        /// Входящее П/П: Взнос в уставный капитал
        /// </summary>
        [Description("Взнос в уставный капитал")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.Ooo)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingContributionToAuthorizedCapital = 114,

        [Description("Выплата по агентскому договору")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingAgencyContract = 116,

        /// <summary>
        /// Входящее П/П: Взнос собственных средств
        /// </summary>
        [Description("Взнос собственных средств")]
        [UserRule]
        [OperationLegal(LegalType.Ip)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingContributionOfOwnFunds = 118,

        /// <summary>
        /// Исходящее П/П: Снятие прибыли
        /// </summary>
        [Description("Снятие прибыли")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.Ip)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingWithdrawalOfProfit = 120,

        /// <summary>
        /// Исходящее П/П: Покупка валюты
        /// </summary>
        [Description("Покупка валюты (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingCurrencyPurchase = 130,

        /// <summary>
        /// Входящее П/П: Поступление от покупки валюты
        /// </summary>
        [Description("Поступление от покупки валюты (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingCurrencyPurchase = 131,

        /// <summary>
        /// Исходящее П/П: Продажа валюты
        /// </summary>
        [Description("Продажа валюты (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingCurrencySale = 132,

        /// <summary>
        /// Входящее П/П: Поступление от продажи валюты
        /// </summary>
        [Description("Поступление от продажи валюты (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingCurrencySale = 133,

        /// <summary>
        /// Валютная комиссия банка
        /// </summary>
        [Description("Списана комиссия банка (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        CurrencyBankFee = 135,

        /// <summary>
        /// Исходящее П/П: Валютный платеж поставщику
        /// </summary>
        [Description("Оплата поставщику (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingCurrencyPaymentToSupplier = 136,

        /// <summary>
        /// Входящее П/П: Валютная оплата от покупателя
        /// </summary>
        [Description("Оплата от покупателя (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingCurrencyPaymentFromCustomer = 138,

        /// <summary>
        /// Исходящее П/П: Перевод на другой валютный счет
        /// </summary>
        [Description("Перевод на другой счет (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [TransferOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingCurrencyTransferToAccount = 139,

        /// <summary>
        /// Входящее П/П: Поступление с другого валютного счета
        /// </summary>
        [Description("Перевод со счета (валюта)")]
        [UserRule]
        [CurrencyOperation]
        [TransferOperation]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingCurrencyTransferFromAccount = 140,

        /// <summary>
        /// Исходящее П/П: Арендный платеж
        /// </summary>
        [Description("Арендный платеж")]
        [UserRule]
        [IpOsnoUnavailable]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingRentPayment = 141,

        /// <summary>
        /// Исходящее П/П: Выдача займа
        /// </summary>
        [Description("Выдача займа")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingLoanIssue = 142,

        /// <summary>
        /// Входящее П/П: Возврат займа или процентов
        /// </summary>
        [Description("Возврат займа или процентов")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingLoanReturn = 143,

        /// <summary>
        /// Входящее П/П: Поступление от комиссионера
        /// </summary>
        [Description("Поступление от комиссионера")]
        [UserRule]
        [AvailableWithContractor]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        [AvailableWithAccessRule(PaymentImportAccessRule.AccessToMarketplacesAndCommissionAgents)]
        PaymentOrderIncomingIncomeFromCommissionAgent = 154,
        
        /// <summary>
        /// Исходящие П/П: Выплаты удержаний
        /// </summary>
        [Description("Выплаты удержаний")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingDeduction = 156,

        /// <summary>
        /// Входящее П/П: Возврат на расчётный счёт
        /// </summary>
        [Description("Возврат на расчётный счёт")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Incoming)]
        PaymentOrderIncomingRefundToSettlementAccount = 157,

        /// <summary>
        /// Исходящие П/П: Единый налоговый платеж
        /// </summary>
        [Description("Единый налоговый платеж")]
        [UserRule]
        [OperationLegal(LegalType.All)]
        [OperationDirection(OperationDirection.Outgoing)]
        PaymentOrderOutgoingUnifiedBudgetaryPayment = 158
    }
}
