using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy
{
    public interface IMoneyOperationDuplicatesApiClient
    {
        Task<DuplicateDetectionResponseDto[]> DetectAsync(FirmId firmId, UserId userId,
            DuplicateDetectionRequestDto request);
    }
}