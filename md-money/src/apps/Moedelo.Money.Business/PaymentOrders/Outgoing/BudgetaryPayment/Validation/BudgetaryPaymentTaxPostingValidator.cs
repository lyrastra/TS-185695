using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.TaxationSystems;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentTaxPostingValidator))]
    internal class BudgetaryPaymentTaxPostingValidator : IBudgetaryPaymentTaxPostingValidator
    {
        private const string message = "Указание НУ вручную невозможно. Расход будет признан при расчете налога в случае выполнения двух условий: начислено и оплачено - датой последнего события, на меньшую из сумм.";

        private readonly IKbkReader kbkReader;
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;

        public BudgetaryPaymentTaxPostingValidator(
            IKbkReader kbkReader,
            IFirmRequisitesReader firmRequisitesReader,
            ITaxationSystemTypeReader taxationSystemTypeReader)
        {
            this.kbkReader = kbkReader;
            this.firmRequisitesReader = firmRequisitesReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
        }

        public async Task ValidateAsync(BudgetaryPaymentSaveRequest request)
        {
            var taxationSystem = await taxationSystemTypeReader.GetFullByYearAsync(request.Date.Year).ConfigureAwait(false);
            if (taxationSystem == null)
            {
                return;
            }

            var isTaxableByHand = await IsTaxableByHandAsync(request, taxationSystem).ConfigureAwait(false);

            if (taxationSystem.IsUsn
                && request.TaxPostings != null
                && request.TaxPostings.UsnTaxPostings.Any()
                && isTaxableByHand == false)
            {
                throw new BusinessValidationException(nameof(request.TaxPostings), message);
            }

            if (taxationSystem.IsOsno
                && request.TaxPostings != null
                && request.TaxPostings.UsnTaxPostings.Any()
                && request.AccountCode != BudgetaryAccountCodes.TradingFees)
            {
                throw new BusinessValidationException(nameof(request.TaxPostings), message);
            }
        }

        private async Task<bool> IsTaxableByHandAsync(BudgetaryPaymentSaveRequest request, TaxationSystem taxationSystem)
        {
            var isOoo = await firmRequisitesReader.IsOooAsync().ConfigureAwait(false);

            if (taxationSystem.IsUsn && taxationSystem.UsnType == Requisites.Enums.TaxationSystems.UsnType.Profit)
            {
                return isOoo
                    ? IsTaxableByHandForOooUsn6(request.AccountCode)
                    : IsTaxableByHandForIpUsn6(request.AccountCode);
            }

            return isOoo
                ? IsTaxableByHandForOooUsn15(request.AccountCode)
                : await IsTaxableByHandForIpUsn15Async(request).ConfigureAwait(false);
        }

        private async Task<bool> IsTaxableByHandForIpUsn15Async(BudgetaryPaymentSaveRequest request)
        {
            var taxableByHandCodes = new[]
            {
                BudgetaryAccountCodes.TradingFees,
                BudgetaryAccountCodes.Eshn,
                BudgetaryAccountCodes.EnvdForUsn,
                BudgetaryAccountCodes.LandTaxes,
                BudgetaryAccountCodes.OtherTaxes
            };
            if (taxableByHandCodes.Contains(request.AccountCode))
            {
                return true;
            }

            if (request.KbkId == null)
            {
                return true;
            }

            var kbk = await kbkReader.GetByIdAsync(request.KbkId.Value);
            if (kbk == null)
            {
                return true;
            }

            if (kbk.KbkPaymentType == KbkPaymentType.Payment &&
                (kbk.KbkType == KbkType.FederalFomsForIp ||
                 kbk.KbkType == KbkType.InsurancePaymentForIp ||
                 kbk.KbkType == KbkType.InsurancePayOverdraft))
            {
                return true;
            }

            if (request.AccountCode == BudgetaryAccountCodes.Nds)
            {
                var hasLinkedCurrencyInvoices =
                    (kbk.KbkType == KbkType.NdsTaxImportToFns || kbk.KbkType == KbkType.NdsTaxOnCustomsHouse) &&
                    kbk.KbkPaymentType == KbkPaymentType.Payment &&
                    request.CurrencyInvoices?.Any() == true;

                return !hasLinkedCurrencyInvoices;
            }

            var taxableByHandKbk = new[]
            {
                KbkType.NdflForIp,
                KbkType.InsurancePaymentForIp,
                KbkType.AccumulatePaymentForIp,
                KbkType.FederalFomsForIp
            };
            if (taxableByHandKbk.Contains(kbk.KbkType))
            {
                return true;
            }

            return false;
        }

        private bool IsTaxableByHandForOooUsn15(BudgetaryAccountCodes accountCode)
        {
            var taxableByHandCodes = new[]
            {
                BudgetaryAccountCodes.Nds,
                BudgetaryAccountCodes.ProfitTaxes,
                BudgetaryAccountCodes.TransportTaxes,
                BudgetaryAccountCodes.PropertyTaxes,
                BudgetaryAccountCodes.TradingFees,
                BudgetaryAccountCodes.Eshn,
                BudgetaryAccountCodes.EnvdForUsn,
                BudgetaryAccountCodes.LandTaxes,
                BudgetaryAccountCodes.OtherTaxes
            };
            return taxableByHandCodes.Contains(accountCode);
        }

        private bool IsTaxableByHandForIpUsn6(BudgetaryAccountCodes accountCode)
        {
            var notTaxableCodes = new[]
            {
                BudgetaryAccountCodes.Ndfl,
                BudgetaryAccountCodes.Nds,
                BudgetaryAccountCodes.Eshn,
                BudgetaryAccountCodes.EnvdForUsn,
                BudgetaryAccountCodes.LandTaxes,
                BudgetaryAccountCodes.OtherTaxes,
                BudgetaryAccountCodes.FssFee,
                BudgetaryAccountCodes.PfrInsuranceFee,
                BudgetaryAccountCodes.PfrAccumulateFee,
                BudgetaryAccountCodes.FomsFee,
                BudgetaryAccountCodes.FssInjuryFee
            };
            return !notTaxableCodes.Contains(accountCode);
        }

        private bool IsTaxableByHandForOooUsn6(BudgetaryAccountCodes accountCode)
        {
            var notTaxableCodes = new[]
            {
                BudgetaryAccountCodes.Ndfl,
                BudgetaryAccountCodes.Nds,
                BudgetaryAccountCodes.ProfitTaxes,
                BudgetaryAccountCodes.TransportTaxes,
                BudgetaryAccountCodes.PropertyTaxes,
                BudgetaryAccountCodes.Eshn,
                BudgetaryAccountCodes.EnvdForUsn,
                BudgetaryAccountCodes.LandTaxes,
                BudgetaryAccountCodes.OtherTaxes,
                BudgetaryAccountCodes.FssFee,
                BudgetaryAccountCodes.PfrInsuranceFee,
                BudgetaryAccountCodes.PfrAccumulateFee,
                BudgetaryAccountCodes.FomsFee,
                BudgetaryAccountCodes.FssInjuryFee
            };
            return !notTaxableCodes.Contains(accountCode);
        }
    }
}
