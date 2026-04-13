using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationDuplicatesReader : IDI
    {
        Task<int?> GetRoboAndSapeIncomingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request);
        Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request);

        Task<int?> GetYandexIncomingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request);
        Task<int?> GetYandexOutgoingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request);
        Task<int?> GetYandexMovementOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request);

        Task<DuplicateResult> GetIncomingOperationIdExtAsync(IUserContext userContext, DuplicateOperationRequest request);
        Task<DuplicateResult> GetOutgoingOperationIdExtAsync(IUserContext userContext, DuplicateOperationRequest request);
    }
}