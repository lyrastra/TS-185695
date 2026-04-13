using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.AdvanceStatements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.AdvanceStatements
{
    [InjectAsSingleton(typeof(AdvanceStatementReader))]
    internal sealed class AdvanceStatementReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAdvanceStatementApiClient advanceStatementApiClient;

        public AdvanceStatementReader(
            IExecutionInfoContextAccessor contextAccessor,
            IAdvanceStatementApiClient advanceStatementApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.advanceStatementApiClient = advanceStatementApiClient;
        }

        public async Task<AdvanceStatement> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await advanceStatementApiClient.GetByBaseIdAsync(context.FirmId, context.UserId, documentBaseId);
            return dto != null
                ? Map(dto)
                : null;
        }

        public async Task<AdvanceStatement[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dto = await advanceStatementApiClient.GetByBaseIdsAsync(context.FirmId, context.UserId, documentBaseIds);
            return dto.Select(Map).ToArray();
        }

        private static AdvanceStatement Map(AdvanceStatementDto dto)
        {
            return new AdvanceStatement
            {
                DocumentBaseId = dto.DocumentBaseId,
                EmployeeId = dto.WorkerId,
            };
        }
    }
}
