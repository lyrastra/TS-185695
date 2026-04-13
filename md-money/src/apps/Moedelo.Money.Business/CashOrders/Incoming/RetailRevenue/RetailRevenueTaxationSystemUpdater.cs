using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.Patent;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Incoming.AccrualOfInterest
{
    [OperationType(OperationType.CashOrderIncomingFromRetailRevenue)]
    [InjectAsSingleton(typeof(IConcreteTaxationSystemUpdater))]
    internal class RetailRevenueTaxationSystemUpdater : IConcreteTaxationSystemUpdater
    {
        private readonly IRetailRevenueReader reader;
        private readonly IRetailRevenueUpdater updater;
        private readonly TaxationSystemChangingAbilityChecker checker;
        private readonly IPatentReader patentReader;

        public RetailRevenueTaxationSystemUpdater(
            IRetailRevenueReader reader,
            IRetailRevenueUpdater updater,
            TaxationSystemChangingAbilityChecker checker,
            IPatentReader patentReader)
        {
            this.reader = reader;
            this.updater = updater;
            this.checker = checker;
            this.patentReader = patentReader;
        }

        public async Task UpdateAsync(long documentBaseId, TaxationSystemType taxationSystemType)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            if (await checker.CanChangeTaxationSystemAsync(response.Date, taxationSystemType) == false)
            {
                return;
            }
            var request = RetailRevenueMapper.MapToSaveRequest(response);
            request.DocumentBaseId = documentBaseId;
            request.TaxationSystemType = taxationSystemType;
            if (taxationSystemType == TaxationSystemType.Patent)
            {
                request.PatentId = await patentReader.GetPatentIdByOperationDateAsync(response.Date);
            }
            else
            {
                request.PatentId = null;
            }
            request.TaxPostingType = ProvidePostingType.Auto;
            await updater.UpdateAsync(request).ConfigureAwait(false);
        }
    }
}
