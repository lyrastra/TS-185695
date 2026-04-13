using Moedelo.AccPostings.Enums;
using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.Enums;
using OperationType = Moedelo.Money.Enums.OperationType;

namespace Moedelo.Money.CashOrders.Business.CashOrders.Extensions
{
    internal static class CashOrderExtensions
    {
        public static SyntheticAccountCode GetSyntheticAccountCode(this CashOrder cashOrder)
        {
            return cashOrder.OperationType switch
            {
                //OperationType.CashOrderOutgoingTranslatedToOtherCash => cashOrder.IsMain
                //    ? SyntheticAccountCode._50_02
                //    : SyntheticAccountCode._50_01,

                //OperationType.CashOrderIncomingLoanObtaining => cashOrder.IsLongTermLoan ?? false
                //    ? SyntheticAccountCode._67_01
                //    : SyntheticAccountCode._66_01,

                //OperationType.CashOrderOutgoingLoanRepayment => cashOrder.IsLongTermLoan ?? false
                //    ? SyntheticAccountCode._67_01
                //    : SyntheticAccountCode._66_01,

                OperationType.CashOrderOutcomingToSettlementAccount or
                OperationType.CashOrderOutgoingCollectionOfMoney or
                OperationType.CashOrderIncomingFromSettlementAccount => SyntheticAccountCode._57_01,

                OperationType.CashOrderOutgoingIssuanceAccountablePerson or
                OperationType.CashOrderIncomingReturnFromAccountablePerson => SyntheticAccountCode._71_01,

                OperationType.CashOrderIncomingFromRetailRevenue => cashOrder.TaxationSystemType == TaxationSystemType.Envd
                    ? SyntheticAccountCode._90_01_02
                    : SyntheticAccountCode._90_01_01,

                //OperationType.CashOrderOutgoingPaymentForWorking => GetCodeCashOrderOutgoingPaymentForWorking(cashOrder),

                OperationType.CashOrderIncomingContributionAuthorizedCapital => SyntheticAccountCode._75_01,

                OperationType.CashOrderOutgoingPaymentAgencyContract => SyntheticAccountCode._76_07,

                OperationType.CashOrderOutgoingProfitWithdrawing or
                OperationType.CashOrderIncomingContributionOfOwnFunds => SyntheticAccountCode._76_06,

                _ => cashOrder.Direction == MoneyDirection.Outgoing
                    ? SyntheticAccountCode._60_02
                    : SyntheticAccountCode._62_02,
            };
        }

        //private static SyntheticAccountCode GetCodeCashOrderOutgoingPaymentForWorking(CashOrder cashOrder)
        //{
        //    if (cashOrder.WorkerDocumentType == null)
        //    {
        //        return SyntheticAccountCode._70_01;
        //    }

        //    return cashOrder.WorkerDocumentType switch
        //    {
        //        PaymentOrderWorkerReasonDocumentType.Gpd => SyntheticAccountCode._76_09,
        //        PaymentOrderWorkerReasonDocumentType.Dividends =>
        //            cashOrder.PayToWorkers.FirstOrDefault()?.IsNotStaff == true
        //                ? SyntheticAccountCode._75_02
        //                : SyntheticAccountCode._70_01,
        //        _ => SyntheticAccountCode._70_01,
        //    };
        //}
    }
}
