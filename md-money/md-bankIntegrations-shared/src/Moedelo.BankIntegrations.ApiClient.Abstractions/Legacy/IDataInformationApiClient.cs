using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.DataInformation;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy;

public interface IDataInformationApiClient
{
    /// <summary>
    /// Получить информацию по интеграциям по р/сч
    /// </summary>
    Task<IntSummaryBySettlementsResponseDto> GetIntSummaryBySettlementsAsync(IntSummaryBySettlementsRequestDto dto);
}