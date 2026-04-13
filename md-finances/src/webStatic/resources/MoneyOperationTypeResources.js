import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import LegalType from '../enums/LegalTypeEnum';
import MoneySourceType from '../enums/MoneySourceType';

const paymentOrderOperationResources = {
    PaymentOrderIncomingPaymentForGoods: {
        value: 16,
        text: `Оплата от покупателя`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `PaymentFromCustomer`
    },
    PaymentOrderIncomingCurrencyPaymentFromBuyer: {
        value: 138,
        text: `Оплата от покупателя`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyPaymentFromCustomer`
    },
    PaymentOrderIncomingReturnFromAccountablePerson: {
        value: 18,
        text: `Возврат от подотчетного лица`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `RefundFromAccountablePerson`
    },
    PaymentOrderIncomingMediationFee: {
        value: 97,
        text: `Посредническое вознаграждение`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `MediationFee`
    },
    MemorialWarrantReceiptFromCash: {
        value: 32,
        text: `Поступление из кассы`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `TransferFromCash`
    },
    MemorialWarrantReceiptGoodsPaidCreditCard: {
        value: 33,
        text: `Эквайринг`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `RetailRevenue`
    },
    PaymentOrderIncomingLoanObtaining: {
        value: 108,
        text: `Получение займа или кредита`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `LoanObtaining`
    },
    PaymentOrderIncomingLoanReturn: {
        value: 143,
        text: `Возврат займа или процентов`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `LoanReturn`
    },
    MemorialWarrantCreditingCollectedFunds: {
        value: 31,
        text: `Инкассированные денежные средства`,
        Direction: Direction.Incoming,
        Available: LegalType.Ooo,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `TransferFromCashCollection`
    },
    PaymentOrderIncomingFromAnotherAccount: {
        value: 10,
        text: `Перевод со счета`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `TransferFromAccount`
    },
    MemorialWarrantAccrualOfInterest: {
        value: 105,
        text: `Начисление процентов от банка`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `AccrualOfInterest`
    },
    PaymentOrderIncomingFromPurse: {
        value: 103,
        text: `Поступление с электронного кошелька`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `TransferFromPurse`
    },
    PaymentOrderIncomingMaterialAid: {
        value: 112,
        text: `Финансовая помощь от учредителя`,
        Direction: Direction.Incoming,
        Available: LegalType.Ooo,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `FinancialAssistance`
    },
    PaymentOrderIncomingContributionOfOwnFunds: {
        value: 118,
        text: `Взнос собственных средств`,
        Direction: Direction.Incoming,
        Available: LegalType.Ip,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `ContributionOfOwnFunds`
    },
    PaymentOrderIncomingContributionAuthorizedCapital: {
        value: 114,
        text: `Взнос в уставный капитал`,
        Direction: Direction.Incoming,
        Available: LegalType.Ooo,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `ContributionToAuthorizedCapital`
    },
    PaymentOrderIncomingCurrencyFromAnotherAccount: {
        value: 140,
        text: `Перевод со счета`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyTransferFromAccount`
    },
    PaymentOrderIncomingCurrencyPurchase: {
        value: 131,
        text: `Поступление от покупки валюты`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyPurchase`
    },
    PaymentOrderIncomingCurrencySale: {
        value: 133,
        text: `Поступление от продажи валюты`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencySale`
    },
    PaymentOrderIncomingIncomeFromCommissionAgent: {
        value: 154,
        text: `Поступление от комиссионера`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `IncomeFromCommissionAgent`
    },
    PaymentOrderRefundToSettlementAccount: {
        value: 157,
        text: `Возврат на расчетный счет`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `RefundToSettlementAccount`
    },
    PaymentOrderIncomingOther: {
        value: 14,
        text: `Прочее`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `Other`
    },
    PaymentOrderIncomingCurrencyOther: {
        value: 134,
        text: `Прочее`, // прочее валютное поступление
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyOther`
    },
    PaymentOrderPaymentToSupplier: {
        value: 26,
        text: `Оплата поставщику`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `PaymentToSupplier`
    },
    PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods: {
        value: 136,
        text: `Оплата поставщику`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyPaymentToSupplier`
    },
    PaymentOrderOutgoingReturnToBuyer: {
        value: 22,
        text: `Возврат покупателю`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `RefundToCustomer`
    },
    BudgetaryPayment: {
        value: 39,
        text: `Бюджетный платеж`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `BudgetaryPayment`
    },
    UnifiedBudgetaryPayment: {
        value: 158,
        text: `Бюджетный платеж`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `UnifiedBudgetaryPayment`
    },
    PaymentToAccountablePerson: {
        value: 23,
        text: `Выдача подотчетному лицу`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `PaymentToAccountablePerson`
    },
    PaymentOrderOutgoingForTransferSalary: {
        value: 28,
        text: `Выплаты физ. лицам`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `PaymentToNaturalPersons`
    },
    PaymentOrderOutgoingDeduction: {
        value: 156,
        text: `Выплаты удержаний`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `Deduction`
    },
    PaymentOrderOutgoingTransferToAccount: {
        value: 29,
        text: `Перевод на другой счет`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `TransferToAccount`
    },
    PaymentOrderOutgoingCurrencyTransferToAccount: {
        value: 139,
        text: `Перевод на другой счет`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyTransferToAccount`
    },
    BankFee: {
        value: 30,
        text: `Списана комиссия банка`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `BankFee`
    },
    PaymentOrderOutgoingCurrencyBankFee: {
        value: 135,
        text: `Списана комиссия банка`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyBankFee`
    },
    WithdrawalFromAccount: {
        value: 34,
        text: `Снятие с расчетного счета`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `WithdrawalFromAccount`
    },
    PaymentOrderOutgoingLoanIssue: {
        value: 142,
        text: `Выдача займа`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `LoanIssue`
    },
    PaymentOrderOutgoingLoanRepayment: {
        value: 109,
        text: `Погашение займа или процентов`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `LoanRepayment`
    },
    PaymentOrderOutgoingPaymentAgencyContract: {
        value: 116,
        text: `Выплата по агентскому договору`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `AgencyContract`
    },
    PaymentOrderOutgoingProfitWithdrawing: {
        value: 120,
        text: `Снятие прибыли`,
        Direction: Direction.Outgoing,
        Available: LegalType.Ip,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `WithdrawalOfProfit`
    },
    PaymentOrderOutgoingCurrencyPurchase: {
        value: 130,
        text: `Покупка валюты`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyPurchase`
    },
    PaymentOrderOutgoingCurrencySale: {
        value: 132,
        text: `Продажа валюты`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencySale`
    },
    RentPayment: {
        value: 141,
        text: `Арендный платеж`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `RentPayment`
    },
    PaymentOrderOutgoingOther: {
        value: 24,
        text: `Прочее`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `Other`
    },
    PaymentOrderOutgoingCurrencyOther: {
        value: 137,
        text: `Прочее`, // прочее валютное списание
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.SettlementAccount,
        RestPath: `CurrencyOther`
    }
};

const cashOrderOperationResources = {
    CashOrderIncomingPaymentForGoods: {
        value: 49,
        text: `Оплата от покупателя`,
        view: `fromKontragent`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingFromSettlementAccount: {
        value: 46,
        text: `Поступление с расчётного счёта`,
        view: `fromSettlementAccount`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingReturnFromAccountablePerson: {
        value: 53,
        text: `Возврат от подотчетного лица`,
        view: `fromAccountablePerson`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingFromRetailRevenue: {
        value: 47,
        text: `Розничная выручка`,
        view: `retailRevenue`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingMediationFee: {
        value: 98,
        text: `Посредническое вознаграждение`,
        view: `mediationFee`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingLoanObtaining: {
        value: 110,
        text: `Получение займа или кредита`,
        view: `incomingLoanObtaining`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingMaterialAid: {
        value: 113,
        text: `Финансовая помощь от учредителя`,
        view: `incomingMaterialAid`,
        Direction: Direction.Incoming,
        Available: LegalType.Ooo,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingFromAnotherCash: {
        value: 45,
        text: `Перемещение из другой кассы`,
        view: `fromOtherCash`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingContributionOfOwnFunds: {
        value: 119,
        text: `Взнос собственных средств`,
        view: `CashOrderIncomingContributionOfOwnFunds`,
        Direction: Direction.Incoming,
        Available: LegalType.Ip,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingContributionAuthorizedCapital: {
        value: 115,
        text: `Взнос в уставный капитал`,
        view: `incomingCashContributing`,
        Direction: Direction.Incoming,
        Available: LegalType.Ooo,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingMiddlemanRetailRevenue: {
        value: 123,
        text: `Розничная выручка по посредническому договору`,
        view: `MiddlemanRetailRevenue`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderIncomingOther: {
        value: 54,
        text: `Прочее`,
        view: `incomingOther`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingPaymentSuppliersForGoods: {
        value: 62,
        text: `Оплата поставщику`,
        view: `toSupplier`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutcomingToSettlementAccount: {
        value: 55,
        text: `Зачисление на расчетный счет`,
        view: `toSettlementAccount`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingIssuanceAccountablePerson: {
        value: 59,
        text: `Выдача подотчетному лицу`,
        view: `toAccountablePerson`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingCollectionOfMoney: {
        value: 64,
        text: `Инкассация денег`,
        view: `moneyCollection`,
        Direction: Direction.Outgoing,
        Available: LegalType.Ooo,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingPaymentForWorking: {
        value: 63,
        text: `Выплаты физ. лицам`,
        view: `salary`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingLoanRepayment: {
        value: 111,
        text: `Погашение займа или процентов`,
        view: `outgoingLoanRepayment`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingReturnToBuyer: {
        value: 58,
        text: `Возврат покупателю`,
        view: `outgoingReturnToBuyer`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingPaymentAgencyContract: {
        value: 117,
        text: `Выплата по агентскому договору`,
        view: `toUnderAgency`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingTranslatedToOtherCash: {
        value: 65,
        text: `Перевод в другую кассу`,
        view: `toOtherCash`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingProfitWithdrawing: {
        value: 121,
        text: `Снятие прибыли`,
        view: `CashOrderOutgoingProfitWithdrawing`,
        Direction: Direction.Outgoing,
        Available: LegalType.Ip,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderOutgoingOther: {
        value: 60,
        text: `Прочее`,
        view: `outgoingOther`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    CashOrderBudgetaryPayment: {
        value: 126,
        text: `Бюджетный платеж`,
        view: `CashBudgetaryPayment`,
        Direction: Direction.Outgoing,
        Available: LegalType.Ip,
        Source: MoneySourceType.Cash,
        RestPath: ``
    },
    UnifiedCashOrderBudgetaryPayment: {
        value: 159,
        text: `Бюджетный платеж`,
        view: `CashBudgetaryPayment`,
        Direction: Direction.Outgoing,
        Available: LegalType.Ip,
        Source: MoneySourceType.Cash,
        RestPath: ``
    }
};

const purseOperationResources = {
    PurseOperationTransferToSettlement: {
        value: 99,
        purseOperationType: 2,
        text: `Перевод на р/с`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Purse,
        RestPath: ``
    },
    PurseOperationComission: {
        value: 100,
        purseOperationType: 3,
        text: `Удержание комиссии`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Purse,
        RestPath: ``
    },
    PurseOperationOtherOutgoing: {
        value: 101,
        purseOperationType: 4, // может быть 101, может быть и 4, мутный кейс
        text: `Прочее`,
        Direction: Direction.Outgoing,
        Available: LegalType.All,
        Source: MoneySourceType.Purse,
        RestPath: ``
    },
    PurseOperationIncome: {
        value: 102,
        purseOperationType: 1,
        text: `Оплата от покупателя`,
        Direction: Direction.Incoming,
        Available: LegalType.All,
        Source: MoneySourceType.Purse,
        RestPath: ``
    }
};

export default { ...paymentOrderOperationResources, ...cashOrderOperationResources, ...purseOperationResources };

export {
    paymentOrderOperationResources,
    cashOrderOperationResources,
    purseOperationResources
};
