using System.Threading.Tasks;

namespace Moedelo.CommonV2.UserContext.Domain.BillingContext;

public interface IFirmBillingContext
{
    Task<IFirmBillingContextData> GetDataAsync();

    void Invalidate();
}