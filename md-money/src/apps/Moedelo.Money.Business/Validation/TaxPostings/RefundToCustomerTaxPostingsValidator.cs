using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation.SimpleValidators;
using Moedelo.Money.Domain.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.Money.Business.Validation.TaxPostings
{
    [InjectAsSingleton(typeof(RefundToCustomerTaxPostingsValidator))]
    class RefundToCustomerTaxPostingsValidator : TaxPostingsValidator
    {
        public RefundToCustomerTaxPostingsValidator(
            IFirmRequisitesReader firmRequisitesReader,
            ITaxationSystemTypeReader taxationSystemTypeReader)
            : base (firmRequisitesReader, taxationSystemTypeReader)
        {
        }

        protected override void ValidateUsn(IReadOnlyList<UsnTaxPosting> postings)
        {
            for (var i = 0; i < postings.Count; i++)
            {
                Validate($"UsnTaxPosting[{i}].Direction", postings[i].Direction, EnumValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Description", postings[i].Description, TaxPostingDescriptionValidator.Validate, XssStringValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Sum", postings[i].Sum, NegativeSumValidator.Validate, MaxPrecisionSumValidator.Validate);
            }
        }

        protected override void ValidateIpOsno(IpOsnoTaxPosting posting)
        {
            if (posting == null)
            {
                return;
            }

            Validate("IpOsnoTaxPosting.Sum", posting.Sum, NegativeSumValidator.Validate, MaxPrecisionSumValidator.Validate);
            Validate("IpOsnoTaxPosting.Direction", posting.Direction, EnumValidator.Validate);
        }
    }
}
