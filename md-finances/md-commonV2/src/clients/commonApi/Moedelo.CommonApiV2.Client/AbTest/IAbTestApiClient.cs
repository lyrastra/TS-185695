using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.AbTest;

namespace Moedelo.CommonApiV2.Client.AbTest;

public interface IAbTestApiClient
{
    Task<AbTestDto> GetAsync(int firmId, int userId, AbTestRequest dto);

    Task<CreateUpdateAbTestDto> CreateOrUpdateAbTestAsync(CreateUpdateAbTestDto requestDto);

    Task<List<CreateUpdateAbTestDto>> GetAllAbTestsAsync();

    Task<bool> DeleteAbTestAsync(int abTestId);

    Task<CreateUpdateAbTestDto> DeleteAbTestVariantAsync(int abTestVariantId);

    /// <summary>
    /// Установить указанный вариант указанного теста для указанного пользователя
    /// </summary>
    Task SetTestVariantAsync(int firmId, int userId, int testId, int variantId);
}