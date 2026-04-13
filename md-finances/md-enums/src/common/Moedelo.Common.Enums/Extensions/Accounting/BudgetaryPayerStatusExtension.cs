using Moedelo.Common.Enums.Enums.Accounting;
using System.Linq;

namespace Moedelo.Common.Enums.Extensions.Accounting
{
    public static class BudgetaryPayerStatusExtension
    {
        private static readonly BudgetaryPayerStatus[] OutdatedIpBudgetaryPayerStatuses = new[]
        {
            BudgetaryPayerStatus.TaxpayerIP,
            BudgetaryPayerStatus.Notary,
            BudgetaryPayerStatus.Lawyer,
            BudgetaryPayerStatus.KfkDirector,
            BudgetaryPayerStatus.CastomsPayment,
            BudgetaryPayerStatus.ResponsiblePartnerKgn,
            BudgetaryPayerStatus.PartnerKgn
        };

        private static readonly BudgetaryPayerStatus[] OutdatedOooBudgetaryPayerStatuses = new[]
{
            BudgetaryPayerStatus.CastomsPayment,
            BudgetaryPayerStatus.ResponsiblePartnerKgn,
            BudgetaryPayerStatus.PartnerKgn
        };

        public static BudgetaryPayerStatus GetActualBudgetaryPayerStatus(this BudgetaryPayerStatus payerStatus, bool isOoo)
        {
            return isOoo ? payerStatus.GetActualOooBudgetaryPayerStatus() : payerStatus.GetActualIpBudgetaryPayerStatus();
        }

        public static BudgetaryPayerStatus GetActualIpBudgetaryPayerStatus(this BudgetaryPayerStatus payerStatus)
        {
            if (OutdatedIpBudgetaryPayerStatuses.Contains(payerStatus))
            {
                return BudgetaryPayerStatus.OtherTaxPayer;
            }

            return payerStatus;
        }

        public static BudgetaryPayerStatus GetActualOooBudgetaryPayerStatus(this BudgetaryPayerStatus payerStatus)
        {
            if (OutdatedOooBudgetaryPayerStatuses.Contains(payerStatus))
            {
                return BudgetaryPayerStatus.Company;
            }

            return payerStatus;
        }
    }
}
