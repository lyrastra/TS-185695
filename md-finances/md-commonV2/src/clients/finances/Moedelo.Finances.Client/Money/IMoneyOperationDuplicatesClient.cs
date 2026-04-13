using System.Threading.Tasks;
using Moedelo.Finances.Dto.Money.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.Money
{
    public interface IMoneyOperationDuplicatesClient : IDI
    {
        Task<int?> GetRoboAndSapeIncomingOperationIdAsync(int firmId, int userId, DuplicateRoboAndSapeOperationRequestDto request);
        Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(int firmId, int userId, DuplicateRoboAndSapeOperationRequestDto request);

        Task<int?> GetYandexIncomingOperationIdAsync(int firmId, int userId, DuplicateYandexOperationRequestDto request);
        Task<int?> GetYandexOutgoingOperationIdAsync(int firmId, int userId, DuplicateYandexOperationRequestDto request);
        Task<int?> GetYandexMovementOperationIdAsync(int firmId, int userId, DuplicateMovementYandexOperationRequestDto request);

        Task<DuplicateResultDto> GetIncomingOperationIdExtAsync(int firmId, int userId, DuplicateIncomingOperationRequestDto request);
        Task<DuplicateResultDto> GetOutgoingOperationIdExtAsync(int firmId, int userId,DuplicateOutgoingOperationRequestDto request);
        Task<DuplicateResultDto> GetBankFeeOutgoingOperationIdExtAsync(int firmId, int userId,DuplicateBankFeeOutgoingOperationRequestDto request);

        Task<DuplicateDetectionResultDto[]> DetectAsync(int firmId, int userId, DuplicateDetectionRequestDto request);
    }
}