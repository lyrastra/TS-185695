using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Remains;

namespace Moedelo.AccountingV2.Client.Remains
{
    public interface IOpeningBalanceApiClient
    {
        Task<OpeningBalancesRepresentation> GetAsync(int firmId, int userId);

        Task SaveAsync(int firmId, int userId, OpeningBalancesSaveRequestModel request);
    }
}
