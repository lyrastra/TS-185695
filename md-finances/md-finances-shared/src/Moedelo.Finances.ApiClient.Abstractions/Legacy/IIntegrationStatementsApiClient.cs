using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy
{
    public interface IIntegrationStatementsApiClient
    {
        /// <summary>
        /// Запросить выписки по р/сч
        /// </summary>
        /// <returns>Для каждого р/сч: статус запроса выписки или причина, почему запросить нельзя</returns>
        Task<ResultOfStatementRequestDto[]> RequestAsync(FirmId firmId, UserId userId, StatementRequestDto request);
    }
}