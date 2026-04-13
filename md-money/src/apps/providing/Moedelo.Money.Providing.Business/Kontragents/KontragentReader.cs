using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Kontragents
{
    [InjectAsSingleton(typeof(KontragentReader))]
    internal class KontragentReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IKontragentsApiClient client;

        public KontragentReader(
            IExecutionInfoContextAccessor contextAccessor,
            IKontragentsApiClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task<Kontragent> GetByIdAsync(int kontragentId)
        {
            var dtos = await client.GetByIdsAsync(
                contextAccessor.ExecutionInfoContext.FirmId,
                contextAccessor.ExecutionInfoContext.UserId,
                new[] { kontragentId });

            return dtos == null || dtos.Length == 0 ? null : Map(dtos.FirstOrDefault());
        }

        private static Kontragent Map(KontragentDto dto)
        {
            return new Kontragent
            {
                Id = dto.Id,
                Name = dto.GetName(),
                SubcontoId = dto.SubcontoId.GetValueOrDefault()
            };
        }
    }
}
