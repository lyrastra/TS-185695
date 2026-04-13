using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;
using Moedelo.BizV2.Dto.TaxSystemSum;
using System.Collections.Generic;

namespace Moedelo.BizV2.Client.TaxSystemSum
{
    public interface ITaxSystemSumApiClient
    {
        Task<List<TaxSystemSumDto>> GetByPeriodAsync(int firmId, int userId, int year, int quarter);
    }
}
