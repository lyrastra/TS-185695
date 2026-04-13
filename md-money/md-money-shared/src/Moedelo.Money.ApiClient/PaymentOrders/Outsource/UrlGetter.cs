using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outsource;

internal static class UrlGetter
{
    internal static string GetConfirmRelativePath(this OperationType type)
    {
        return $"/private/api/v1/PaymentOrders/{GetConfirmUrlOperationPart(type)}/Outsource/Confirm";
    }

    private static string GetConfirmUrlOperationPart(OperationType type)
    {
        return type switch
        {
            // поступления 
            OperationType.MemorialWarrantAccrualOfInterest => "Incoming/AccrualOfInterest",
            OperationType.PaymentOrderIncomingPaymentFromCustomer => "Incoming/PaymentFromCustomer",
            OperationType.MemorialWarrantRetailRevenue => "Incoming/RetailRevenue",
            OperationType.PaymentOrderIncomingRefundFromAccountablePerson => "Incoming/RefundFromAccountablePerson",
            OperationType.PaymentOrderIncomingOther => "Incoming/OtherIncoming",
            OperationType.MemorialWarrantTransferFromCash => "Incoming/TransferFromCash",
            OperationType.MemorialWarrantTransferFromCashCollection => "Incoming/TransferFromCashCollection",
            OperationType.PaymentOrderIncomingMediationFee => "Incoming/MediationFee",
            OperationType.PaymentOrderIncomingLoanObtaining => "Incoming/LoanObtaining",
            OperationType.PaymentOrderIncomingContributionOfOwnFunds => "Incoming/ContributionOfOwnFunds",
            OperationType.PaymentOrderIncomingTransferFromPurse => "Incoming/TransferFromPurse",
            OperationType.PaymentOrderIncomingFinancialAssistance => "Incoming/FinancialAssistance",
            OperationType.PaymentOrderIncomingContributionToAuthorizedCapital => "Incoming/ContributionToAuthorizedCapital",
            OperationType.PaymentOrderIncomingIncomeFromCommissionAgent => "Incoming/IncomeFromCommissionAgent",
            OperationType.PaymentOrderIncomingRefundToSettlementAccount => "Incoming/RefundToSettlementAccount",
            OperationType.PaymentOrderIncomingLoanReturn => "Incoming/LoanReturn",
            OperationType.PaymentOrderIncomingTransferFromAccount => "Incoming/TransferFromAccount",
            // списания
            OperationType.MemorialWarrantBankFee => "Outgoing/BankFee",
            OperationType.PaymentOrderOutgoingPaymentToSupplier => "Outgoing/PaymentToSupplier",
            OperationType.PaymentOrderOutgoingOther => "Outgoing/OtherOutgoing",
            OperationType.PaymentOrderOutgoingWithdrawalOfProfit => "Outgoing/WithdrawalOfProfit",
            OperationType.PaymentOrderOutgoingPaymentToAccountablePerson => "Outgoing/PaymentToAccountablePerson",
            OperationType.PaymentOrderOutgoingRentPayment => "Outgoing/RentPayment",
            OperationType.PaymentOrderOutgoingLoanRepayment => "Outgoing/LoanRepayment",
            OperationType.BudgetaryPayment => "Outgoing/BudgetaryPayment",
            OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment => "Outgoing/UnifiedBudgetaryPayment",
            OperationType.PaymentOrderOutgoingRefundToCustomer => "Outgoing/RefundToCustomer",
            OperationType.PaymentOrderOutgoingLoanIssue => "Outgoing/LoanIssue",
            OperationType.MemorialWarrantWithdrawalFromAccount => "Outgoing/WithdrawalFromAccount",
            OperationType.PaymentOrderOutgoingAgencyContract => "Outgoing/AgencyContract",
            OperationType.PaymentOrderOutgoingPaymentToNaturalPersons => "Outgoing/PaymentToNaturalPersons",
            OperationType.PaymentOrderOutgoingTransferToAccount => "Outgoing/TransferToAccount",
            OperationType.PaymentOrderOutgoingDeduction => "Outgoing/Deduction",
            // ... валюта
            OperationType.CurrencyBankFee => "Outgoing/CurrencyBankFee",
            OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier => "Outgoing/CurrencyPaymentToSupplier",
            OperationType.PaymentOrderOutgoingCurrencyTransferToAccount => "Outgoing/CurrencyTransferToAccount",
            OperationType.PaymentOrderOutgoingCurrencySale => "Outgoing/CurrencySale",
            OperationType.PaymentOrderOutgoingCurrencyPurchase => "Outgoing/CurrencyPurchase", // рублевая
            OperationType.PaymentOrderOutgoingCurrencyOther => "Outgoing/CurrencyOtherOutgoing",
            OperationType.PaymentOrderIncomingCurrencyTransferFromAccount => "Incoming/CurrencyTransferFromAccount",
            OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer => "Incoming/CurrencyPaymentFromCustomer",
            OperationType.PaymentOrderIncomingCurrencyPurchase => "Incoming/CurrencyPurchase",
            OperationType.PaymentOrderIncomingCurrencySale => "Incoming/CurrencySale", // рублевая
            OperationType.PaymentOrderIncomingCurrencyOther => "Incoming/CurrencyOtherIncoming",
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Тип {type} недоступен/нереализован для операции подтверждения п/п")
        };
    }
}