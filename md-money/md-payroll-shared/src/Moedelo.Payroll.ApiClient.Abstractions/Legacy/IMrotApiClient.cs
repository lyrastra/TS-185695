using System;
using System.Threading.Tasks;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IMrotApiClient
    {
        Task<decimal?> GetRegionalMrotSum(string regionCode, DateTime? date = null);
        
        Task<decimal?> GetFederalMrotSum(DateTime? date = null);
    }
}