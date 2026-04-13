using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System;

namespace Moedelo.Money.Business.Kbks
{
    public class Kbk
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public KbkType KbkType { get; set; }
        public KbkPaymentType KbkPaymentType { get; set; }
        public BudgetaryFundType FundType { get; set; }
        public BudgetaryPaymentBase PaymentBase { get; set; }
        public BudgetaryPayerStatus PayerStatus { get; set; }
        public KbkUsingType KbkUsingType { get; set; }
        public string DocNumber { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public BudgetaryAccountCodes AccountCode { get; set; }
        public string Purpose { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long SubcontoId { get; set; }
        public string Description { get; set; }
    }

    public static class KbkExtensions
    {
        public static bool IsActual(this Kbk kbk, DateTime date)
        {
            return (kbk.ActualStartDate == null || kbk.ActualStartDate <= date) &&
                   (kbk.ActualEndDate == null || kbk.ActualEndDate >= date);
        }

        public static string GetSubcontoName(this Kbk kbk)
        {
            if (kbk.AccountCode.IsSocialInsurance())
            {
                return kbk.Description;
            }

            if ((BudgetaryAccountCodes)kbk.AccountCode == BudgetaryAccountCodes.EnvdForUsn)
            {
                return GetSubcontoNameFor68_12(kbk.KbkType, kbk.Description, kbk.KbkPaymentType);
            }

            return $"{kbk.Description} КБК {kbk.Number}";
        }

        private static string GetSubcontoNameFor68_12(KbkType kbkType, string description, Enums.KbkPaymentType kbkPaymentType)
        {
            return kbkType == KbkType.DeclarationUsnProfitOutgoMinTax
                ? description
                : $"{GetPaymentTypeName(kbkPaymentType)} {GetKbkNumberTypeName(kbkType)}";
        }

        private static string GetPaymentTypeName(Enums.KbkPaymentType kbkPaymentType)
        {
            switch (kbkPaymentType)
            {
                case KbkPaymentType.Payment: return "Налог";
                case KbkPaymentType.Surcharge: return "Пени";
                case KbkPaymentType.Forfeit: return "Штрафы";
                default:
                    throw new ArgumentOutOfRangeException(nameof(kbkPaymentType), kbkPaymentType, null);
            }
        }

        private static string GetKbkNumberTypeName(KbkType kbkType)
        {
            switch (kbkType)
            {
                case KbkType.DeclarationUsn6: return "УСН (доходы)";
                case KbkType.DeclarationUsn15: return "УСН (доходы минус расходы)";
                default:
                    throw new ArgumentOutOfRangeException(nameof(kbkType), kbkType, null);
            }
        }
    }
}
