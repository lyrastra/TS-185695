using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.PrimaryDocuments
{
    public interface IPrimaryDocumentsApiClient : IDI
    {
        Task ReplaceKontragentInPrimaryDocumentsAsync(int firmId, int userId, KontragentReplaceDto request);

        Task DeleteNotBindedToBalancesAsync(int firmId, int userId);
    }
}
