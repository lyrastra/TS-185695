using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation.SimpleValidators;
using Moedelo.Money.Domain.TaxPostings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Validation.TaxPostings
{
    [InjectAsSingleton(typeof(TaxPostingsValidator))]
    class TaxPostingsValidator
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;

        public TaxPostingsValidator(IFirmRequisitesReader firmRequisitesReader, ITaxationSystemTypeReader taxationSystemTypeReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
        }

        public Task ValidateAsync(DateTime documentDate, TaxPostingsData taxPostings)
        {
            return ValidateAsync(documentDate, null, taxPostings.UsnTaxPostings, taxPostings.OsnoTaxPostings,
                taxPostings.IpOsnoTaxPosting, null);
        }

        public Task ValidateAsync(DateTime documentDate, TaxationSystemType? taxationSystemType, TaxPostingsData taxPostings)
        {
            return ValidateAsync(documentDate, taxationSystemType, taxPostings.UsnTaxPostings, taxPostings.OsnoTaxPostings,
                taxPostings.IpOsnoTaxPosting, taxPostings.PatentTaxPostings);
        }

        private async Task ValidateAsync(
            DateTime documentDate,
            TaxationSystemType? taxationSystemType,
            IReadOnlyCollection<UsnTaxPosting> usnTaxPostings,
            IReadOnlyCollection<OsnoTaxPosting> osnoTaxPostings,
            IpOsnoTaxPosting ipOsnoTaxPosting,
            IReadOnlyCollection<PatentTaxPosting> patentTaxPostings)
        {
            var isOooTask = firmRequisitesReader.IsOooAsync();
            var taxationSystemTask = taxationSystemTypeReader.GetFullByYearAsync(documentDate.Year);

            await Task.WhenAll(isOooTask, taxationSystemTask);

            var isOoo = isOooTask.Result;
            var taxationSystem = taxationSystemTask.Result;

            if (taxationSystem.IsUsn)
            {
                ValidateUsn(usnTaxPostings.ToList());
                return;
            }

            if (taxationSystem.IsOsno && isOoo)
            {
                ValidateOsno(osnoTaxPostings.ToList());
                return;
            }

            if (taxationSystem.IsOsno && !isOoo && taxationSystemType == TaxationSystemType.Patent)
            {
                ValidatePatent(patentTaxPostings.ToList());
                return;
            }

            if (taxationSystem.IsOsno && isOoo == false)
            {
                ValidateIpOsno(ipOsnoTaxPosting);
                return;
            }
        }

        protected virtual void ValidateUsn(IReadOnlyList<UsnTaxPosting> postings)
        {
            for (var i = 0; i < postings.Count; i++)
            {
                Validate($"UsnTaxPosting[{i}].Direction", postings[i].Direction, EnumValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Description", postings[i].Description, TaxPostingDescriptionValidator.Validate, XssStringValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Sum", postings[i].Sum, SumValidator.Validate, MaxPrecisionSumValidator.Validate);
            }
        }

        protected virtual void ValidateIpOsno(IpOsnoTaxPosting posting)
        {
            if (posting == null)
            {
                return;
            }

            Validate("IpOsnoTaxPosting.Sum", posting.Sum, SumValidator.Validate, MaxPrecisionSumValidator.Validate);
            Validate("IpOsnoTaxPosting.Direction", posting.Direction, EnumValidator.Validate);
        }

        private static void ValidateOsno(IReadOnlyList<OsnoTaxPosting> postings)
        {
            for (var i = 0; i < postings.Count; i++)
            {
                Validate($"OsnoTaxPosting[{i}].Sum", postings[i].Sum, SumValidator.Validate, MaxPrecisionSumValidator.Validate);
                Validate($"OsnoTaxPosting[{i}].Direction", postings[i].Direction, EnumValidator.Validate);
                Validate($"OsnoTaxPosting[{i}].Type", postings[i].Type, EnumValidator.Validate);
                Validate($"OsnoTaxPosting[{i}].Kind", postings[i].Kind, EnumValidator.Validate);
                Validate($"OsnoTaxPosting[{i}].NormalizedCostType", postings[i].NormalizedCostType, EnumValidator.Validate);
            }
        }

        private static void ValidatePatent(IReadOnlyList<PatentTaxPosting> postings)
        {
            for (var i = 0; i < postings.Count; i++)
            {
                Validate($"OsnoTaxPosting[{i}].Sum", postings[i].Sum, SumValidator.Validate, MaxPrecisionSumValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Description", postings[i].Description, TaxPostingDescriptionValidator.Validate, XssStringValidator.Validate);
            }
        }

        protected static void Validate<T>(string memberName, T value, params Func<string, T, string>[] validators)
        {
            foreach (var validator in validators)
            {
                var validateMessage = validator(memberName, value);
                if (!string.IsNullOrEmpty(validateMessage))
                {
                    throw new BusinessValidationException(memberName, validateMessage);
                }
            }
        }
    }
}
