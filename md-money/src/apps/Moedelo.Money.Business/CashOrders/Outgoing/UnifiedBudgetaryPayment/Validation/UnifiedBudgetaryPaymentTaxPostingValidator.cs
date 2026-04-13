using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.TaxationSystems;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment.Validation
{
    [InjectAsSingleton(typeof(UnifiedBudgetaryPaymentTaxPostingValidator))]
    internal class UnifiedBudgetaryPaymentTaxPostingValidator
    {
        private const string message = "Указание НУ вручную невозможно. Расход будет признан при расчете налога в случае выполнения двух условий: начислено и оплачено - датой последнего события, на меньшую из сумм.";

        private readonly IKbkReader kbkReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;

        public UnifiedBudgetaryPaymentTaxPostingValidator(
            IKbkReader kbkReader,
            ITaxationSystemTypeReader taxationSystemTypeReader)
        {
            this.kbkReader = kbkReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
        }

        public async Task ValidateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var taxationSystem = await taxationSystemTypeReader.GetFullByYearAsync(request.Date.Year);
            if (taxationSystem == null)
            {
                return;
            }

            int i = 0;
            foreach (var subPayment in request.SubPayments)
            {
                var kbk = await kbkReader.GetByIdAsync(subPayment.KbkId);

                var isTaxableByHand = IsTaxableByHand(kbk, taxationSystem);

                if (taxationSystem.IsUsn
                    && subPayment.TaxPostings != null
                    && subPayment.TaxPostings.UsnTaxPostings.Any()
                    && isTaxableByHand == false)
                {
                    throw new BusinessValidationException($"SubPayments[{i}].TaxPostings", message);
                }

                if (taxationSystem.IsOsno
                    && subPayment.TaxPostings != null
                    && subPayment.TaxPostings.UsnTaxPostings.Any())
                {
                    throw new BusinessValidationException($"SubPayments[{i}].TaxPostings", message);
                }

                i++;
            }
        }

        private static bool IsTaxableByHand(Kbk kbk, TaxationSystem taxationSystem)
        {
            if (taxationSystem.IsUsn == false)
            {
                return false;
            }

            return taxationSystem.UsnType == Requisites.Enums.TaxationSystems.UsnType.ProfitAndOutgo 
                && IsTaxableByHandForIpUsn15(kbk);
        }

        private static bool IsTaxableByHandForIpUsn15(Kbk kbk)
        {
            if (kbk == null)
            {
                return true;
            }

            var taxableByHandKbk = new[]
            {
                KbkType.FederalFomsForIp,
                KbkType.InsurancePaymentForIp,
                KbkType.InsurancePayOverdraft
            };
            if (taxableByHandKbk.Contains(kbk.KbkType))
            {
                return true;
            }

            return false;
        }
    }
}
