using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.TaxationSystems
{
    [InjectAsSingleton(typeof(ITaxationSystemTypeReader))]
    internal class TaxationSystemTypeReader : ITaxationSystemTypeReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxationSystemApiClient taxationSystemClient;

        public TaxationSystemTypeReader(
            IExecutionInfoContextAccessor contextAccessor,
            ITaxationSystemApiClient taxationSystemClient)
        {
            this.contextAccessor = contextAccessor;
            this.taxationSystemClient = taxationSystemClient;
        }

        public async Task<Enums.TaxationSystemType?> GetByYearAsync(int year)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var taxSystem = await taxationSystemClient.GetByYearAsync(context.FirmId, context.UserId, year).ConfigureAwait(false);
            return (Enums.TaxationSystemType?)taxSystem?.ToTaxationSystemType();
        }

        public async Task<Enums.TaxationSystemType?> GetDefaultByYearAsync(int year)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var taxSystem = await taxationSystemClient.GetByYearAsync(context.FirmId, context.UserId, year).ConfigureAwait(false);
            return (Enums.TaxationSystemType?)taxSystem?.ToDefaultTaxationSystemType();
        }
    }
}
