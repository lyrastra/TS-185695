using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.CatalogV2.Client.Funds
{
    public interface IFundSettlementAccountApiClient : IDI
    {
        Task<string> MatchAsync(string settlementAccount);
    }
}