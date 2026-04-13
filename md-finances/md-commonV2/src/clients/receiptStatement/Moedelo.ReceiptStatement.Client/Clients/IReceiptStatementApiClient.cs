using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.ReceiptStatement.Dto;

namespace Moedelo.ReceiptStatement.Client.Clients
{
    public interface IReceiptStatementApiClient : IDI
    {
        Task<ReceiptStatementDto> GetAsync(int firmId, int userId, long documentBaseId);

        Task<List<ReceiptStatementBySubcontoDto>> GetBaseIdsBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds);

        Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId);
    }
}
