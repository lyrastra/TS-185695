using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Patent
{
    [InjectAsSingleton(typeof(IPatentReader))]
    internal class PatentReader : IPatentReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPatentApiClient patentApiClient;

        public PatentReader(
            IExecutionInfoContextAccessor contextAccessor,
            IPatentApiClient patentApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.patentApiClient = patentApiClient;
        }

        public async Task<bool> IsAnyExists(int year)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return await patentApiClient.IsAnyExists(context.FirmId, context.UserId, year);
        }

        public async Task<bool> IsAnyExists(DateTime date)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var patents = await patentApiClient.GetWithoutAdditionalDataAsync(context.FirmId, context.UserId, date.Year);
            return patents.Any(x => x.StartDate <= date && date <= x.EndDate);
        }

        public async Task<long?> GetPatentIdByOperationDateAsync(DateTime date)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var patents = await patentApiClient.GetWithoutAdditionalDataAsync(context.FirmId, context.UserId, date.Year);

            var activePatents = patents.Where(patent => date >= patent.StartDate && date <= patent.EndDate).ToArray();

            if (activePatents.Length == 1)
            {
                return activePatents.First().Id;
            }

            return null;
        }
    }
}