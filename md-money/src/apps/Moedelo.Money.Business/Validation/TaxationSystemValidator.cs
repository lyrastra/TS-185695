using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(ITaxationSystemValidator))]
    internal sealed class TaxationSystemValidator : ITaxationSystemValidator
    {
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly IPatentReader patentReader;

        public TaxationSystemValidator(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IFirmRequisitesReader firmRequisitesReader,
            IPatentReader patentReader)
        {
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.firmRequisitesReader = firmRequisitesReader;
            this.patentReader = patentReader;
        }

        public async Task ValidateAsync(DateTime date, TaxationSystemType taxationSystemType)
        {
            var taxationSystem = await taxationSystemTypeReader.GetByYearAsync(date.Year);

            if (taxationSystemType == TaxationSystemType.Patent)
            {
                await ValidateForPatentAsync(date, taxationSystem);
                return;
            }

            if (IsAvailable(taxationSystem, taxationSystemType) == false)
            {
                throw new BusinessValidationException("TaxationSystemType", "Нельзя использовать указанную СНО");
            }
        }

        public async Task ValidateUsnAsync(int year)
        {
            var taxationSystem = await taxationSystemTypeReader.GetFullByYearAsync(year);

            if (!taxationSystem.IsUsn)
            {
                throw new BusinessValidationException("TaxationSystemType", "Доступ только для СНО УСН");
            }
        }

        private async Task ValidateForPatentAsync(DateTime date, TaxationSystemType? taxationSystem)
        {
            if (IsAvailableForPatent(taxationSystem) == false)
            {
                throw new BusinessValidationException("TaxationSystemType", "Нельзя использовать указанную СНО");
            }

            var isOoo = await firmRequisitesReader.IsOooAsync();
            if (isOoo)
            {
                throw new BusinessValidationException("TaxationSystemType", "Нельзя использовать патент для ООО");
            }

            var isPatentExists = await patentReader.IsAnyExists(date);
            if (isPatentExists == false)
            {
                throw new BusinessValidationException("TaxationSystemType", "Нет патента на указанную дату");
            }
        }

        private static bool IsAvailableForPatent(TaxationSystemType? taxationSystem)
        {
            return taxationSystem == TaxationSystemType.Usn ||
                   taxationSystem == TaxationSystemType.Envd ||
                   taxationSystem == TaxationSystemType.UsnAndEnvd ||
                   taxationSystem == TaxationSystemType.Osno;
        }


        private static bool IsAvailable(TaxationSystemType? requisitesTaxSystem, TaxationSystemType moneyTaxSystem)
        {
            switch (requisitesTaxSystem)
            {
                case TaxationSystemType.Usn:
                    return moneyTaxSystem == TaxationSystemType.Usn;
                case TaxationSystemType.Osno:
                    return moneyTaxSystem == TaxationSystemType.Osno;
                case TaxationSystemType.Envd:
                    return moneyTaxSystem == TaxationSystemType.Envd;
                case TaxationSystemType.UsnAndEnvd:
                    return moneyTaxSystem == TaxationSystemType.Usn ||
                           moneyTaxSystem == TaxationSystemType.Envd ||
                           moneyTaxSystem == TaxationSystemType.UsnAndEnvd;
                case TaxationSystemType.OsnoAndEnvd:
                    return moneyTaxSystem == TaxationSystemType.Osno ||
                           moneyTaxSystem == TaxationSystemType.Envd ||
                           moneyTaxSystem == TaxationSystemType.OsnoAndEnvd;
                default:
                    return false;
            }
        }
    }
}