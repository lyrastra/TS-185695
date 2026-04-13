using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;

namespace Moedelo.CommonV2.UserContext.BillingContext;

public interface IFirmBillingContextCachingReader
{
    Task<IFirmBillingContextData> GetAsync(int firmId, int userId);
    Task InvalidateAsync(int firmId);
    Task InvalidateAsync(int firmId, int userId);
    Task InvalidateAsync(IReadOnlyCollection<int> firmIds, int userId);
}