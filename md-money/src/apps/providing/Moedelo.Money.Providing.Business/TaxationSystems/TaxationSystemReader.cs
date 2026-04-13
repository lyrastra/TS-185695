using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.TaxationSystems;
using System.Threading.Tasks;
using TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType;

namespace Moedelo.Money.Providing.Business.TaxationSystems
{
    [InjectAsSingleton(typeof(TaxationSystemReader))]
    class TaxationSystemReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxationSystemApiClient taxationSystemClient;

        public TaxationSystemReader(
            IExecutionInfoContextAccessor contextAccessor,
            ITaxationSystemApiClient taxationSystemClient)
        {
            this.contextAccessor = contextAccessor;
            this.taxationSystemClient = taxationSystemClient;
        }

        public async Task<TaxationSystem> GetByYearAsync(int year)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var taxSystem = await taxationSystemClient.GetByYearAsync(context.FirmId, context.UserId, year).ConfigureAwait(false);
            return taxSystem != null
                ? Map(taxSystem)
                : null;
        }

        private static TaxationSystem Map(TaxationSystemDto dto)
        {
            return new TaxationSystem
            {
                Id = dto.Id,
                StartYear = dto.StartYear,
                EndYear = dto.EndYear,
                IsUsn = dto.IsUsn,
                IsUsnProfitAndOutgo = dto.UsnType == UsnType.ProfitAndOutgo,
                UsnSize = dto.UsnSize,
                IsEnvd = dto.IsEnvd,
                IsOsno = dto.IsOsno,
                TaxationSystemType = dto.ToTaxationSystemType(),
                DefaultTaxationSystemType = dto.ToDefaultTaxationSystemType(),
            };
        }
    }
}
