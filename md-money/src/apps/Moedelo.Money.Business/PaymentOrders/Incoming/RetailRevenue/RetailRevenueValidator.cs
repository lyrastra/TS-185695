using System.Collections.Generic;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Accounting.Domain.Shared.NdsRates;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueValidator))]
    internal sealed class RetailRevenueValidator : IRetailRevenueValidator
    {
        private static readonly IReadOnlyList<NdsType?> AllowedAcquiringCommissionNdsRates =
        [
            null,
            NdsType.Nds22,
            NdsType.Nds22To122
        ];
        
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly IDateLessFirmRegistrationValidator dateLessFirmRegistrationValidator;
        private readonly PatentValidator patentValidator;

        public RetailRevenueValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator,
            IDateLessFirmRegistrationValidator dateLessFirmRegistrationValidator,
            PatentValidator patentValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.dateLessFirmRegistrationValidator = dateLessFirmRegistrationValidator;
            this.patentValidator = patentValidator;
        }

        public async Task ValidateAsync(RetailRevenueSaveRequest request)
        {
            var minDateProp = new[]
            {
                new { Value = request.Date, Name = nameof(request.Date) },
                new { Value = request.SaleDate, Name = nameof(request.SaleDate) },
                new { Value = request.AcquiringCommissionDate ?? request.Date, Name = nameof(request.AcquiringCommissionDate) }
            }.MinBy(p => p.Value);

            await closedPeriodValidator.ValidateAsync(minDateProp.Value, minDateProp.Name);
            await dateLessFirmRegistrationValidator.ValidateAsync(minDateProp.Value, minDateProp.Name);

            ValidateAcquiringCommissionNds(request);

            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value);
            }
            await patentValidator.ValidateAsync(request.PatentId);
        }

        private static void ValidateAcquiringCommissionNds(RetailRevenueSaveRequest request)
        {
            if (!request.IncludeNds)
            {
                return;
            }

            var commissionDate = request.AcquiringCommissionDate ?? request.Date;
            const string propName = "Nds";

            if (commissionDate < RetailRevenueNdsStartDate.StartDate)
            {
                throw new BusinessValidationException(propName, $"НДС в эквайринге доступен с {RetailRevenueNdsStartDate.StartDate:d}");
            }

            if (request.AcquiringCommissionSum > 0 && request.NdsSum > request.AcquiringCommissionSum.Value)
            {
                throw new BusinessValidationException(propName, "Сумма НДС комиссии должна быть меньше самой комиссии");
            }

            if (!AllowedAcquiringCommissionNdsRates.Contains(request.NdsType))
            {
                throw new BusinessValidationException(propName, $"Некорректная ставка НДС комиссии: {request.NdsType}. Доступны: {string.Join(", ", AllowedAcquiringCommissionNdsRates)}");
            }
        }
    }
}