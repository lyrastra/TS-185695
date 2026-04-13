using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Contracts
{
    [InjectAsSingleton(typeof(IContractsReader))]
    internal sealed class ContractsReader : IContractsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IContractsApiClient contractsClient;

        public ContractsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IContractsApiClient contractsClient)
        {
            this.contextAccessor = contextAccessor;
            this.contractsClient = contractsClient;
        }

        public async Task<Contract> GetByBaseIdAsync(long baseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dtos = await contractsClient.GetByBaseIdsAsync(context.FirmId, context.UserId, new[] { baseId });
            var dto = dtos.FirstOrDefault();
            return dto != null
                ? Map(dto)
                : null;
        }

        public async Task<IReadOnlyCollection<Contract>> GetByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dtos = await contractsClient.GetByBaseIdsAsync(context.FirmId, context.UserId, baseIds);
            return dtos.Select(Map).ToArray();
        }

        public async Task<Contract> GetMainContractAsync(int kontragentId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await contractsClient.GetOrCreateMainContractAsync(context.FirmId, context.UserId, kontragentId);
            return Map(dto);
        }

        private static Contract Map(ContractDto dto)
        {
            return new Contract
            {
                Id = dto.Id,
                DocumentBaseId = dto.DocumentBaseId,
                KontragentId = dto.KontragentId ?? 0,
                ContractKind = dto.ContractKind,
                IsMainContract = dto.IsMainContract,
                SubcontoId = dto.SubcontoId
            };
        }
    }
}
