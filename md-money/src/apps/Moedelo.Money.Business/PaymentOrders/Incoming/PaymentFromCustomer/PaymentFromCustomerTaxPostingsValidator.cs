using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation.SimpleValidators;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerTaxPostingsValidator))]
    class PaymentFromCustomerTaxPostingsValidator : TaxPostingsValidator
    {
        public PaymentFromCustomerTaxPostingsValidator(
            IFirmRequisitesReader firmRequisitesReader,
            ITaxationSystemTypeReader taxationSystemTypeReader)
            : base(firmRequisitesReader, taxationSystemTypeReader)
        {
        }

        protected override void ValidateUsn(IReadOnlyList<UsnTaxPosting> postings)
        {
            for (var i = 0; i < postings.Count; i++)
            {
                Validate($"UsnTaxPosting[{i}].Direction", postings[i].Direction, EnumValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Description", postings[i].Description, TaxPostingDescriptionHardValidator.Validate, XssStringValidator.Validate);
                Validate($"UsnTaxPosting[{i}].Sum", postings[i].Sum, MaxPrecisionSumValidator.Validate);
            }
        }
    }
}