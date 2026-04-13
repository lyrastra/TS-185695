using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.ClosedPeriod;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(TaxationSystemChangingAbilityChecker))]
    internal class TaxationSystemChangingAbilityChecker
    {
        private readonly IClosedPeriodReader closedPeriodReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IPatentReader patentReader;

        public TaxationSystemChangingAbilityChecker(
            IClosedPeriodReader closedPeriodReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IPatentReader patentReader)
        {
            this.closedPeriodReader = closedPeriodReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.patentReader = patentReader;
        }

        public async Task<bool> CanChangeTaxationSystemAsync(DateTime date, TaxationSystemType taxationSystemType)
        {
            var lastClosedDate = await closedPeriodReader.GetLastClosedDateAsync().ConfigureAwait(false);
            if (date <= lastClosedDate)
            {
                return false;
            }
            var firmTaxationSystem = await taxationSystemTypeReader.GetByYearAsync(date.Year).ConfigureAwait(false);
            if (firmTaxationSystem?.CanChangeTaxationSystem(taxationSystemType) == false)
            {
                return false;
            }
            if (taxationSystemType == TaxationSystemType.Patent)
            {
                return await patentReader.IsAnyExists(date);
            }
            return true;
        }
    }
}
