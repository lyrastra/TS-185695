// ReSharper disable InconsistentNaming
using System.ComponentModel;

namespace Moedelo.Changelog.Shared.Domain.Enums
{
    public enum ChangeLogEntityType
    {
        #region Documents (1..99)
        [Description("Входящий акт")]
        PurchaseStatement = 1,
        [Description("Исходящий акт")]
        SaleStatement = 2,
        [Description("Входящая накладная")]
        PurchaseWaybill = 3,
        [Description("Исходящая накладная")]
        SaleWaybill = 4,
        [Description("Входящий УПД")]
        PurchaseUpd = 5,
        [Description("Исходящий УПД")]
        SaleUpd = 6,
        #endregion

        #region Money (100..499)

        #region Money_CashOrders (100..199)

        #region Money_CashOrders_Incoming (100..149)

        [Description("Взнос собственных средств")]
        Money_CashOrders_Incoming_ContributionOfOwnFunds = 100,

        [Description("Взнос в уставный капитал")]
        Money_CashOrders_Incoming_ContributionToAuthorizedCapital = 101,

        [Description("Финансовая помощь от учредителя")]
        Money_CashOrders_Incoming_FinancialAssistance = 102,

        [Description("Получение займа или кредита")]
        Money_CashOrders_Incoming_LoanObtaining = 103,

        [Description("Посредническое вознаграждение")]
        Money_CashOrders_Incoming_MediationFee = 104,

        [Description("Розничная выручка по посредническому договору")]
        Money_CashOrders_Incoming_MiddlemanRetailRevenue = 105,

        [Description("Прочее поступление")]
        Money_CashOrders_Incoming_Other = 106,

        [Description("Оплата от покупателя")]
        Money_CashOrders_Incoming_PaymentFromCustomer = 107,

        [Description("Возврат от подотчетного лица")]
        Money_CashOrders_Incoming_RefundFromAccountablePerson = 108,

        [Description("Розничная выручка")]
        Money_CashOrders_Incoming_RetailRevenue = 109,

        [Description("Поступление с расчётного счёта")]
        Money_CashOrders_Incoming_TransferFromSettlementAccount = 110,

        [Description("Перемещение из другой кассы")]
        Money_CashOrders_Incoming_TransferFromCash = 111,

        #endregion Money_CashOrders_Incoming

        #region Money_CashOrders_Outgoing (150..199)

        [Description("Выплата по агентскому договору")]
        Money_CashOrders_Outgoing_AgencyContract = 150,

        [Description("Выплата по агентскому договору")]
        Money_CashOrders_Outgoing_BudgetaryPayment = 151,

        [Description("Инкассация денег")]
        Money_CashOrders_Outgoing_CashCollection = 152,

        [Description("Погашение займа или процентов")]
        Money_CashOrders_Outgoing_LoanRepayment = 153,

        [Description("Прочее списание")]
        Money_CashOrders_Outgoing_Other = 154,

        [Description("Выдача подотчетному лицу")]
        Money_CashOrders_Outgoing_PaymentToAccountablePerson = 155,

        [Description("Выплаты физ. лицам")]
        Money_CashOrders_Outgoing_PaymentToNaturalPersons = 156,

        [Description("Оплата поставщику")]
        Money_ChashOrders_Outgoing_PaymentToSupplier = 157,

        [Description("Возврат покупателю")]
        Money_ChashOrders_Outgoing_RefundToCustomer = 158,

        [Description("Зачисление на расчетный счет")]
        Money_CashOrders_Outgoing_TransferToSettlementAccount = 159,

        [Description("Снятие прибыли")]
        Money_CashOrders_Outgoing_WithdrawalOfProfit = 160,

        [Description("Перевод в другую кассу")]
        Money_CashOrders_Outgoing_TransferToCash = 161,

        [Description("Единый налоговый платеж")]
        Money_CashOrders_Outgoing_UnifiedBudgetaryPayment = 162,

        #endregion Money_CashOrders_Outgoing

        #endregion Money_CashOrders

        #region Money_PaymentOrders (200..299)

        #region Money_PaymentOrders_Incomiing (200..249)

        [Description("Оплата от покупателя в валюте")]
        Money_PaymentOrders_Incoming_CurrencyPaymentFromCustomer = 200,

        [Description("Оплата от покупателя")]
        Money_PaymentOrders_Incoming_PaymentFromCustomer = 201,

        [Description("Эквайринг")]
        Money_PaymentOrders_Incoming_RetailRevenue = 202,

        [Description("Начисление процентов от банка")]
        Money_PaymentOrders_Incoming_AccrualOfInterest =203,

        [Description("Взнос собственных средств")]
        Money_PaymentOrders_Incoming_ContributionOfOwnFunds = 204,

        [Description("Взнос в уставный капитал")]
        Money_PaymentOrders_Incoming_ContributionToAuthorizedCapital = 205,

        [Description("Финансовая помощь от учредителя")]
        Money_PaymentOrders_Incoming_FinancialAssistance = 206,

        [Description("Поступление от комиссионера")]
        Money_PaymentOrders_Incoming_IncomeFromCommissionAgent = 207,

        [Description("Получение займа или кредита")]
        Money_PaymentOrders_Incoming_LoanObtaining = 208,

        [Description("Возврат займа или процентов")]
        Money_PaymentOrders_Incoming_LoanReturn = 209,

        [Description("Посредническое вознаграждение")]
        Money_PaymentOrders_Incoming_MediationFee = 210,

        [Description("Прочее поступление")]
        Money_PaymentOrders_Incoming_OtherIncoming = 211,

        [Description("Возврат от подотчетного лица")]
        Money_PaymentOrders_Incoming_RefundFromAccountablePerson = 212,

        [Description("Перевод со счета")]
        Money_PaymentOrders_Incoming_TransferFromAccount = 213,

        [Description("Поступление из кассы")]
        Money_PaymentOrders_Incoming_TransferFromCash = 214,

        [Description("Инкассированные денежные средства")]
        Money_PaymentOrders_Incoming_TransferFromCashCollection = 215,

        [Description("Поступление с электронного кошелька")]
        Money_PaymentOrders_Incoming_TransferFromPurse = 216,

        [Description("Прочее поступление в валюте")]
        Money_PaymentOrders_Incoming_CurrencyOtherIncoming = 217,

        [Description("Поступление от покупки валюты")]
        Money_PaymentOrders_Incoming_CurrencyPurchase = 218,

        [Description("Поступление от продажи валюты")]
        Money_PaymentOrders_Incoming_CurrencySale = 219,

        [Description("Валютный перевод со счета")]
        Money_PaymentOrders_Incoming_CurrencyTransferFromAccount = 220,

        [Description("Возврат на расчётный счёт")]
        Money_PaymentOrders_Incoming_RefundToSettlementAccount = 221,

        #endregion Money_PaymentOrders_Incomiing

        #region Money_PaymentOrders_Outgoing (250..299)

        [Description("Бюджетный платеж")]
        Money_PaymentOrders_Outgoing_BudgetaryPayment = 250,

        [Description("Оплата поставщику")]
        Money_PaymentOrders_Outgoing_PaymentToSupplier = 251,

        [Description("Списана комиссия банка")]
        Money_PaymentOrders_Outgoing_BankFee = 252,

        [Description("Выплата по агентскому договору")]
        Money_PaymentOrders_Outgoing_AgencyContract = 253,

        [Description("Выдача займа")]
        Money_PaymentOrders_Outgoing_LoanIssue = 254,

        [Description("Погашение займа или процентов")]
        Money_PaymentOrders_Outgoing_LoanRepayment = 255,

        [Description("Прочее списание")]
        Money_PaymentOrders_Outgoing_OtherOutgoing = 256,

        [Description("Выдача подотчетному лицу")]
        Money_PaymentOrders_Outgoing_PaymentToAccountablePerson = 257,

        [Description("Выплаты физ. лицам")]
        Money_PaymentOrders_Outgoing_PaymentToNaturalPersons = 258,

        [Description("Возврат покупателю")]
        Money_PaymentOrders_Outgoing_RefundToCustomer = 259,

        [Description("Арендный платеж")]
        Money_PaymentOrders_Outgoing_RentPayment = 260,

        [Description("Перевод на другой счет")]
        Money_PaymentOrders_Outgoing_TransferToAccount = 261,

        [Description("Снятие с расчетного счета")]
        Money_PaymentOrders_Outgoing_WithdrawalFromAccount = 262,

        [Description("Снятие прибыли")]
        Money_PaymentOrders_Outgoing_WithdrawalOfProfit = 263,

        [Description("Списана комиссия банка в валюте")]
        Money_PaymentOrders_Outgoing_CurrencyBankFee = 264,

        [Description("Прочее списание в валюте")]
        Money_PaymentOrders_Outgoing_CurrencyOther = 265,

        [Description("Оплата поставщику в валюте")]
        Money_PaymentOrders_Outgoing_CurrencyPaymentToSupplier = 266,

        [Description("Покупка валюты")]
        Money_PaymentOrders_Outgoing_CurrencyPurchase = 267,

        [Description("Продажа валюты")]
        Money_PaymentOrders_Outgoing_CurrencySale = 268,

        [Description("Перевод на другой счет в валюте")]
        Money_PaymentOrders_Outgoing_CurrencyTransferToAccount = 269,

        [Description("Выплата удержаний")]
        Money_PaymentOrders_Outgoing_Deduction = 270,

        [Description("Единый налоговый платеж")]
        Money_PaymentOrders_Outgoing_UnifiedBudgetaryPayment = 271,

        #endregion Money_PaymentOrders_Outgoing

        #endregion Money_PaymentOrders

        #region Money_PurseOperations (300..399)

        #region Money_PurseOperations_Incoming (300..349)

        [Description("Оплата от покупателя")]
        Money_PurseOperations_Incoming_PaymentFromCustomer = 300,

        #endregion Money_PurseOperations_Incoming

        #region Money_PurseOperations_Outgoing (350..399)

        [Description("Прочее списание")]
        Money_PurseOperations_Outgoing_Other = 350,

        [Description("Перевод на р/с")]
        Money_PurseOperations_Outgoing_TransferToSettlementAccount = 351,

        [Description("Удержание комиссии")]
        Money_PurseOperations_Outgoing_WithholdingOfFee = 352,

        #endregion Money_PurseOperations_Outgoing

        #endregion

        #endregion Money

        #region Requisites (500..599)
        [Description("Событие календаря")]
        CalendarEvent = 500,
        [Description("Видимость склада")]
        StockVisibility = 501,
        #endregion
    }
}
