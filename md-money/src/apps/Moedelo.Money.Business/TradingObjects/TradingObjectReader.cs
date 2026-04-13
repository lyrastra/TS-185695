using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.TradingObjects
{
    [InjectAsSingleton(typeof(ITradingObjectReader))]
    internal class TradingObjectReader : ITradingObjectReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITradingObjectApiClient tradingObjectApiClient;

        public TradingObjectReader(
            IExecutionInfoContextAccessor contextAccessor,
            ITradingObjectApiClient tradingObjectApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.tradingObjectApiClient = tradingObjectApiClient;
        }

        public async Task<TradingObject> GetByIdAsync(int tradingObjectId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await tradingObjectApiClient.GetByIdAsync(context.FirmId, context.UserId, tradingObjectId);
            return dto != null
                ? Map(dto)
                : null;
        }

        private static TradingObject Map(TradingObjectDto dto)
        {
            return new TradingObject
            {
                Id = dto.Id,
                Number = dto.Number
            };
        }
    }
}
