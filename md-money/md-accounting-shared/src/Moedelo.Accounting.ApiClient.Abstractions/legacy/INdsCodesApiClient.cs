using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface INdsCodesApiClient
    {
        Task<List<NdsCodeDto>> GetByDateAsync(int firmId, int userId, DateTime date);
    }
}