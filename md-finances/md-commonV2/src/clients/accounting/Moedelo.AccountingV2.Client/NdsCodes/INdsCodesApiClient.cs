using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.NdsCodes;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.NdsCodes
{
    public interface INdsCodesApiClient : IDI
    {
        Task<List<NdsCodeDto>> GetByDateAsync(int firmId, int userId, DateTime date);
    }
}
