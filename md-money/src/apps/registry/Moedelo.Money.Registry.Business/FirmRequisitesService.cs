using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Business
{
    [InjectAsSingleton(typeof(IFirmRequisitesService))]
    public class FirmRequisitesService : IFirmRequisitesService
    {
        private static readonly DateTime DateOfStartService = new DateTime(2010, 1, 1);

        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IFirmRequisitesApiClient requisitesApiClient;

        public FirmRequisitesService(
            IFirmRequisitesApiClient requisitesApiClient, 
            IExecutionInfoContextAccessor executionInfoContext)
        {
            this.requisitesApiClient = requisitesApiClient;
            this.executionInfoContext = executionInfoContext;
        }

        public async Task<DateTime> GetFirmRegistrationAsync()
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var requisites = await requisitesApiClient.GetRegistrationDataAsync(context.FirmId, context.UserId).ConfigureAwait(false);

            return requisites?.RegistrationDate ?? DateOfStartService;
        }
    }
}
