using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.FirmRequisites
{
    [InjectAsSingleton(typeof(IFirmRequisitesReader))]
    internal sealed class FirmRequisitesReader : IFirmRequisitesReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IFirmRequisitesApiClient client;

        private readonly AsyncLocal<RegistrationData> regData = new();

        public FirmRequisitesReader(
            IExecutionInfoContextAccessor contextAccessor,
            IFirmRequisitesApiClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        private async Task<RegistrationData> GetRegistrationDataAsync()
        {
            if (regData.Value != null)
            {
                return regData.Value;
            }

            var context = contextAccessor.ExecutionInfoContext;
            var dto = await client.GetRegistrationDataAsync(context.FirmId, context.UserId);
            if (dto != null)
            {
                regData.Value = Map(dto);
            }
            return regData.Value;
        }

        private static RegistrationData Map(RegistrationDataDto dto)
        {
            return new RegistrationData
            {
                IsOoo = dto.IsOoo,
                RegistrationDate = dto.RegistrationDate
            };
        }

        public async Task<bool> IsOooAsync()
        {
            var regData = await GetRegistrationDataAsync();
            return regData?.IsOoo ?? false;
        }

        public async Task<DateTime?> GetRegistrationDateAsync()
        {
            var regData = await GetRegistrationDataAsync();
            return regData?.RegistrationDate;
        }
    }
}
