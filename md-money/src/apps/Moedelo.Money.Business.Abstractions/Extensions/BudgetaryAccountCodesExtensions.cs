using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Extensions
{
    public static class BudgetaryAccountCodesExtensions
    {
        public static bool PaymentLater2017(this BudgetaryAccountCodes accountCode, DateTime paymentDate)
        {
            if (accountCode == BudgetaryAccountCodes.EnvdForUsn ||
                (paymentDate >= new DateTime(2017, 1, 1) &&
                (accountCode == BudgetaryAccountCodes.FssFee ||
                accountCode == BudgetaryAccountCodes.PfrInsuranceFee ||
                accountCode == BudgetaryAccountCodes.PfrAccumulateFee ||
                accountCode == BudgetaryAccountCodes.FomsFee)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool PaymentLater2023(this BudgetaryAccountCodes accountCode, DateTime paymentDate)
        {
            if (paymentDate >= new DateTime(2023, 1, 1) &&
                (accountCode != BudgetaryAccountCodes.Eshn ||
                accountCode != BudgetaryAccountCodes.TouristTaxes))
            {
                return true;
            }

            return false;
        }

        public static bool PaymentLater2025(this BudgetaryAccountCodes accountCode, DateTime paymentDate)
        {
            if (paymentDate >= new DateTime(2025, 1, 1) &&
                (accountCode == BudgetaryAccountCodes.Eshn ||
                accountCode == BudgetaryAccountCodes.TouristTaxes))
            {
                return true;
            }

            return false;
        }
    }
}
