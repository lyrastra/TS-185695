using Microsoft.Extensions.Logging;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.Contracts
{
    [InjectAsSingleton(typeof(ContractReader))]
    internal class ContractReader
    {
        private readonly ILogger logger;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IContractsApiClient client;

        public ContractReader(
            ILogger<ContractReader> logger,
            IExecutionInfoContextAccessor contextAccessor,
            IContractsApiClient client)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task<Contract> GetByBaseIdAsync(long baseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var contracts = await client.GetByBaseIdsAsync(
                context.FirmId,
                context.UserId,
                new List<long> { baseId }).ConfigureAwait(false);
            if(contracts.FirstOrDefault() == null)
                logger.LogInformation($"Искомый договор удален. baseId={baseId}; firmId={context.FirmId}; userId={context.UserId}"); ;
            return Map(contracts.FirstOrDefault());
        }

        public async Task<Contract> GetMainAsync(int kontragentId)
        {
            var dto = await client.GetOrCreateMainContractAsync(
                contextAccessor.ExecutionInfoContext.FirmId,
                contextAccessor.ExecutionInfoContext.UserId,
                kontragentId).ConfigureAwait(false);

            return Map(dto);
        }

        // Provider сам решает, что делать с ошибкой чтения договора:
        // завершить обработку сообщения без retry, снять providing state и т.д.
        public async Task<ContractResolveResult> TryGetAsync(long? contractBaseId, int kontragentId)
        {
            try
            {
                var contract = contractBaseId.HasValue
                    ? await GetByBaseIdAsync(contractBaseId.Value).ConfigureAwait(false) ??
                      await GetMainAsync(kontragentId).ConfigureAwait(false)
                    : await GetMainAsync(kontragentId).ConfigureAwait(false);

                return ContractResolveResult.Success(contract);
            }
            catch (HttpRequestResponseStatusException ex)
                when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                logger.LogError(ex, "Не удалось получить контракт");
                return ContractResolveResult.Fail();
            }
            catch (System.Exception ex)
            {
                logger.LogCritical(ex, "Ошибка при получении контракта");
                return ContractResolveResult.Fail();
            }
        }

        private static Contract Map(ContractDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Contract
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Name = dto.Name ?? $"Договор №{dto.Number} от {dto.Date:dd.MM.yyyy}",
                SubcontoId = dto.SubcontoId.GetValueOrDefault(),
                IsMainContract = dto.IsMainContract
            };
        }
    }
}
