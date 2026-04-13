using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [OperationType(OperationType.MemorialWarrantAccrualOfInterest)]
    [InjectAsSingleton(typeof(IConcreteTaxationSystemUpdater))]
    internal class AccuralOfInterestTaxationSystemUpdater : IConcreteTaxationSystemUpdater
    {
        private readonly IAccrualOfInterestReader reader;
        private readonly IAccrualOfInterestUpdater updater;
        private readonly TaxationSystemChangingAbilityChecker checker;

        public AccuralOfInterestTaxationSystemUpdater(
            IAccrualOfInterestReader reader,
            IAccrualOfInterestUpdater updater,
            TaxationSystemChangingAbilityChecker checker)
        {
            this.reader = reader;
            this.updater = updater;
            this.checker = checker;
        }

        public async Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType)
        {
            if (taxationSystemType == TaxationSystemType.Patent)
            {
                return;
            }
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            if (await checker.CanChangeTaxationSystemAsync(response.Date, taxationSystemType).ConfigureAwait(false) == false)
            {
                return;
            }
            var request = AccrualOfInterestMapper.MapToSaveRequest(response);
            request.DocumentBaseId = documentBaseId;
            request.TaxationSystemType = taxationSystemType;
            request.TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };
            await updater.UpdateAsync(request).ConfigureAwait(false);
        }
    }
}
