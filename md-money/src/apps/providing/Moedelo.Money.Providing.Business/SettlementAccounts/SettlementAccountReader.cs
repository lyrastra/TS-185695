using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.SettlementAccounts
{
    [InjectAsSingleton(typeof(SettlementAccountReader))]
    class SettlementAccountReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountClient;

        public SettlementAccountReader(
            IExecutionInfoContextAccessor contextAccessor,
            ISettlementAccountApiClient settlementAccountClient)
        {
            this.contextAccessor = contextAccessor;
            this.settlementAccountClient = settlementAccountClient;
        }

        public async Task<SettlementAccount> GetByIdAsync(int settlementAccountId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await settlementAccountClient.GetByIdAsync(context.FirmId, context.UserId, settlementAccountId);
            return Map(dto);
        }

        private SettlementAccount Map(SettlementAccountDto dto)
        {
            return new SettlementAccount
            {
                Number = dto.Number,
                SubcontoId = dto.SubcontoId.GetValueOrDefault()
            };
        }
    }
}
