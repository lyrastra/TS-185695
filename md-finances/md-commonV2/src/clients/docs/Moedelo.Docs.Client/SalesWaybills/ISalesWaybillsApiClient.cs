using Moedelo.Docs.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.SalesWaybills
{
    public interface ISalesWaybillsApiClient : IDI
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}