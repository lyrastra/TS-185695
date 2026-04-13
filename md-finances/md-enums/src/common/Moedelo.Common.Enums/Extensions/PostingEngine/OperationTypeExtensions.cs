using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Common.Enums.Extensions.PostingEngine
{
    public static class OperationTypeExtensions
    {
        static OperationTypeExtensions()
        {
            OtherPaymentTypes = new List<OperationType>
            {
                OperationType.PaymentOrderOutgoingOther,
                OperationType.PaymentOrderIncomingOther,
                OperationType.CashOrderIncomingOther,
                OperationType.CashOrderOutgoingOther,
                OperationType.PurseOperationOtherOutgoing
            };

            ByHandTaxable = new List<OperationType>
            {
                OperationType.PaymentOrderOutgoingForTransferSalary,
                OperationType.PaymentOrderOutgoingIssuanceAccountablePerson,
                OperationType.CashOrderOutgoingPaymentForWorking,
                OperationType.CashOrderOutgoingIssuanceAccountablePerson
            };

            PaymentForGoods = new List<OperationType>
            {
                OperationType.PaymentOrderIncomingPaymentForGoods,
                OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods,

                OperationType.CashOrderIncomingPaymentForGoods,
                OperationType.CashOrderOutgoingPaymentSuppliersForGoods,

                OperationType.PurseOperationIncome
            };

            PaymentForAccountablePerson = new List<OperationType>
            {
                OperationType.CashOrderIncomingReturnFromAccountablePerson,
                OperationType.CashOrderOutgoingIssuanceAccountablePerson,

                OperationType.PaymentOrderIncomingReturnFromAccountablePerson,
                OperationType.PaymentOrderOutgoingIssuanceAccountablePerson
            };
        }

        public static List<OperationType> PaymentForAccountablePerson { get; }

        public static List<OperationType> PaymentForGoods { get; }

        public static List<OperationType> OtherPaymentTypes { get; }

        public static List<OperationType> ByHandTaxable { get; }

        public static bool IsPaymentTypeTaxbleByHand(this OperationType type)
        {
            return OtherPaymentTypes.Contains(type) || ByHandTaxable.Contains(type);
        }

        public static bool IsOtherPaymentType(this OperationType type)
        {
            return OtherPaymentTypes.Contains(type);
        }

        /// <summary> Переводы м-ду расчетными счетами </summary>
        public static bool IsOperationBetweenSettlementAccounts(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingTransferToOtherAccount ||
                   type == OperationType.PaymentOrderIncomingFromAnotherAccount;
        }

        /// <summary> Внутренняя операция: взаимодействие кассы и банка </summary>
        public static bool IsOperationBetweenBankAndCash(this OperationType type)
        {
            return type == OperationType.PaymentOrderIncomingTransferFromCash ||
                   type == OperationType.PaymentOrderOutgoingtWithdrawalFromAccount;
        }

        /// <summary> Взаимодействие с поставщиками/покупателями </summary>
        public static bool IsPaymentForGoods(this OperationType type)
        {
            return PaymentForGoods.Contains(type);
        }

        /// <summary> Операции с подотчетными лицами </summary>
        public static bool IsPaymentForAccountablePerson(this OperationType type)
        {
            return PaymentForAccountablePerson.Contains(type);
        }

        /// <summary> Выплата З/П </summary>
        public static bool IsTransferSalary(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingForTransferSalary ||
                   type == OperationType.CashOrderOutgoingPaymentForWorking;
        }

        /// <summary> п/п для сотрудника (или подотчетного лица) </summary>
        public static bool IsWorkerOperation(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingForTransferSalary ||
                   type == OperationType.CashOrderOutgoingPaymentForWorking ||
                   type == OperationType.PaymentOrderOutgoingIssuanceAccountablePerson ||
                   type == OperationType.PaymentOrderIncomingReturnFromAccountablePerson ||
                   type == OperationType.CashOrderIncomingReturnFromAccountablePerson ||
                   type == OperationType.CashOrderOutgoingIssuanceAccountablePerson;
        }

        /// <summary> ПКО (розничная выручка) </summary>
        public static bool IsRetailRevenue(this OperationType type)
        {
            return type == OperationType.CashOrderIncomingFromRetailRevenue ||
                   type == OperationType.CashOrderIncomingMiddlemanRetailRevenue;
        }

        public static bool IsTypeWithDefaultContract(this OperationType type)
        {
            return !type.IsOtherPaymentType() && !type.IsPaymentForGoods() &&
                   !type.IsLoanObtaining() && !type.IsLoanRepayment() &&
                   !type.IsReturnToBuyer() && !type.IsAgencyContract() &&
                   !type.IsLoanIssue() && !type.IsLoanReturn();
        }

        public static bool IsIncomingMovePaymentOrder(this OperationType type)
        {
            return type == OperationType.PaymentOrderIncomingFromAnotherAccount;
        }

        public static bool IsMediationFee(this OperationType type)
        {
            return type == OperationType.CashOrderIncomingMediationFee ||
                   type == OperationType.PaymentOrderIncomingMediationFee ||
                   type == OperationType.CashOrderIncomingMiddlemanRetailRevenue;
        }

        /// <summary> Получение займа </summary>
        public static bool IsLoanObtaining(this OperationType type)
        {
            return type == OperationType.PaymentOrderIncomingLoanObtaining ||
                   type == OperationType.CashOrderIncomingLoanObtaining;
        }

        /// <summary> Погашение займа </summary>
        public static bool IsLoanRepayment(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingLoanRepayment ||
                   type == OperationType.CashOrderOutgoingLoanRepayment;
        }

        public static bool IsLoanIssue(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingLoanIssue;
        }

        public static bool IsLoanReturn(this OperationType type)
        {
            return type == OperationType.PaymentOrderIncomingLoanReturn;
        }

        public static bool IsReturnToBuyer(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingReturnToBuyer ||
                   type == OperationType.CashOrderOutgoingReturnToBuyer;
        }

        public static bool IsAgencyContract(this OperationType type)
        {
            return type == OperationType.PaymentOrderOutgoingPaymentAgencyContract ||
                   type == OperationType.CashOrderOutgoingPaymentAgencyContract;
        }

        public static bool IsMemorialWarrant(this OperationType type)
        {
            switch (type)
            {
                case OperationType.PaymentOrderOutgoingBankFee:
                case OperationType.MemorialWarrantCreditingCollectedFunds:
                case OperationType.PaymentOrderIncomingTransferFromCash:
                case OperationType.PaymentOrderIncomingRetailRevenue:
                case OperationType.PaymentOrderOutgoingtWithdrawalFromAccount:
                case OperationType.PaymentOrderIncomingAccrualOfInterest:
                    return true;
                default: return false;
            }
        }

        public static bool IsPurseOperation(this OperationType type)
        {
            switch (type)
            {
                case OperationType.PurseOperationComission:
                case OperationType.PurseOperationIncome:
                case OperationType.PurseOperationOtherOutgoing:
                case OperationType.PurseOperationTransferToSettlement:
                    return true;
                default: return false;
            }
        }

        public static bool IsCurrencyOperation(this OperationType type)
        {
            switch (type)
            {
                case OperationType.PaymentOrderOutgoingPurchaseCurrency:
                case OperationType.PaymentOrderIncomingPurchaseCurrency:
                case OperationType.PaymentOrderOutgoingSaleCurrency:
                case OperationType.PaymentOrderIncomingSaleCurrency:
                case OperationType.PaymentOrderIncomingCurrencyOther:
                case OperationType.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods:
                case OperationType.CurrencyBankFee:
                case OperationType.PaymentOrderOutgoingCurrencyOther:
                case OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer:
                case OperationType.PaymentOrderIncomingCurrencyTransferFromAccount:
                case OperationType.PaymentOrderOutgoingCurrencyTransferToAccount:
                    return true;
                default: return false;
            }
        }
    }
}