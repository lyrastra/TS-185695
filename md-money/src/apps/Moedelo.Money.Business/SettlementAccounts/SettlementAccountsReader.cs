using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.SettlementAccounts
{
    [InjectAsSingleton(typeof(ISettlementAccountsReader))]
    internal sealed class SettlementAccountsReader : ISettlementAccountsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;

        public SettlementAccountsReader(
            IExecutionInfoContextAccessor contextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
        }

        public async Task<SettlementAccount> GetByIdAsync(int settlementAccountId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await settlementAccountApiClient.GetByIdAsync(context.FirmId, context.UserId, settlementAccountId).ConfigureAwait(false);
            return dto != null
               ? Map(dto)
               : null;
        }

        private static SettlementAccount Map(SettlementAccountDto dto)
        {
            return new SettlementAccount
            {
                Id = dto.Id,
                Currency = dto.Currency,
                Type = dto.Type,
                BankId = dto.BankId,
                SubcontoId = dto.SubcontoId
            };
        }
    }
}
