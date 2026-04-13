using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.Enums;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.Validation.Kontragents;
using Moedelo.Money.Domain;
using KontragentWithRequisites = Moedelo.Money.Domain.KontragentWithRequisites;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(IKontragentsValidator))]
    internal sealed class KontragentsValidator : IKontragentsValidator
    {
        private readonly IKontragentsReader kontragentsReader;

        public KontragentsValidator(IKontragentsReader kontragentsReader)
        {
            this.kontragentsReader = kontragentsReader;
        }

        public async Task<Kontragent> ValidateAsync(int kontragentId)
        {
            var kontragent = await kontragentsReader.GetByIdAsync(kontragentId).ConfigureAwait(false);
            if (kontragent == null)
            {
                throw new BusinessValidationException("Contractor.Id", $"Не найден контрагент с ид {kontragentId}");
            }
            if (kontragent.SubcontoId == null)
            {
                throw new BusinessValidationException("Contractor.Id", $"Отсутствует субконто контрагента с ид {kontragentId}");
            }
            return kontragent;
        }

        public Task<Kontragent> ValidateAsync(KontragentWithRequisites kontragentWithRequisites)
        {
            return ValidateInternalAsync(kontragentWithRequisites);
        }

        public Task<Kontragent> ValidateAsync(ContractorWithRequisites contractorWithRequisites)
        {
            return ValidateInternalAsync(contractorWithRequisites);
        }

        private async Task<Kontragent> ValidateInternalAsync(IContractorWithRequisites contractorWithRequisites)
        {
            var kontragent = await ValidateAsync(contractorWithRequisites.Id);
            switch (kontragent.Form)
            {
                case KontragentForm.UL:
                    KontragentNameValidator.Validate1000(contractorWithRequisites.Name);
                    KontragentInnValidator.Validate10(contractorWithRequisites.Inn);
                    KontragentBankBikValidator.Validate(contractorWithRequisites.BankBik, contractorWithRequisites.IsCurrency);
                    KontragentSettlementAccountValidator.Validate(contractorWithRequisites.SettlementAccount);
                    KontragentSettlementAccountValidator.Validate(contractorWithRequisites.BankCorrespondentAccount);
                    break;

                case KontragentForm.IP:
                    KontragentNameValidator.Validate1000(contractorWithRequisites.Name);
                    KontragentInnValidator.Validate12(contractorWithRequisites.Inn);
                    KontragentBankBikValidator.Validate(contractorWithRequisites.BankBik, contractorWithRequisites.IsCurrency);
                    KontragentSettlementAccountValidator.Validate(contractorWithRequisites.SettlementAccount);
                    KontragentSettlementAccountValidator.Validate(contractorWithRequisites.BankCorrespondentAccount);
                    break;

                case KontragentForm.FL:
                    KontragentNameValidator.Validate256(contractorWithRequisites.Name);
                    KontragentInnValidator.Validate12(contractorWithRequisites.Inn);
                    KontragentBankBikValidator.Validate(contractorWithRequisites.BankBik, contractorWithRequisites.IsCurrency);
                    KontragentSettlementAccountValidator.Validate(contractorWithRequisites.SettlementAccount);
                    KontragentSettlementAccountValidator.Validate(contractorWithRequisites.BankCorrespondentAccount);
                    break;
            }
            return kontragent;
        }
    }
}
