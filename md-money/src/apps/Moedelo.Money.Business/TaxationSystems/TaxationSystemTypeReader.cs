using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.TaxationSystems;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.TaxationSystems
{
    [InjectAsSingleton(typeof(ITaxationSystemTypeReader))]
    internal sealed class TaxationSystemTypeReader : ITaxationSystemTypeReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxationSystemApiClient taxationSystemClient;

        private readonly AsyncLocal<TaxationSystemDto[]> taxSystems = new();

        public TaxationSystemTypeReader(
            IExecutionInfoContextAccessor contextAccessor,
            ITaxationSystemApiClient taxationSystemClient)
        {
            this.contextAccessor = contextAccessor;
            this.taxationSystemClient = taxationSystemClient;
        }

        public async Task<Enums.TaxationSystemType?> GetByYearAsync(int year)
        {
            var taxSystem = await GetInternalByYearAsync(year);
            return (Enums.TaxationSystemType?)taxSystem?.ToTaxationSystemType();
        }

        public async Task<Enums.TaxationSystemType?> GetDefaultByYearAsync(int year)
        {
            var taxSystem = await GetInternalByYearAsync(year);
            return (Enums.TaxationSystemType?)taxSystem?.ToDefaultTaxationSystemType();
        }

        public async Task<TaxationSystem> GetFullByYearAsync(int year)
        {
            var taxSystem = await GetInternalByYearAsync(year);
            return taxSystem != null
                ? Map(taxSystem)
                : null;
        }

        private async Task<TaxationSystemDto> GetInternalByYearAsync(int year)
        {
            var taxSystems = await GetInternalAsync();
            return taxSystems.FirstOrDefault(x => x.StartYear <= year && (x.EndYear == null || x.EndYear > year));
        }

        private async Task<TaxationSystemDto[]> GetInternalAsync()
        {
            if (taxSystems.Value != null)
            {
                return taxSystems.Value;
            }

            var context = contextAccessor.ExecutionInfoContext;
            var response = await taxationSystemClient.GetAsync(context.FirmId, context.UserId);

            taxSystems.Value = response ?? Array.Empty<TaxationSystemDto>();

            return taxSystems.Value;
        }

        private static TaxationSystem Map(TaxationSystemDto dto)
        {
            return new TaxationSystem
            {
                Id = dto.Id,
                StartYear = dto.StartYear,
                EndYear = dto.EndYear,
                IsUsn = dto.IsUsn,
                UsnType = dto.UsnType,
                IsEnvd = dto.IsEnvd,
                IsOsno = dto.IsOsno
            };
        }
    }
}
