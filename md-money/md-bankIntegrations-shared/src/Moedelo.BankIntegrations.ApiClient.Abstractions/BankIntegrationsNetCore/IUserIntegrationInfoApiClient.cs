using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;

public interface IUserIntegrationInfoApiClient
{
    /// <summary> Получение данных для панели интеграции в деньгах </summary>
    /// <param name="firmId">Идентификатор фирмы</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Разрешенные и подключенные банки. Состояние счетчика</returns>
    Task<UserIntegrationInfoDto> GetDataAsync(int firmId, int userId);
    
    /// <summary> Получение данных об интеграциях для реквизитов в мобильном приложении </summary>
    /// <param name="firmId">Идентификатор фирмы</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns> Подключенные, разрешенные и неразрешенные интеграции </returns>
    Task<UserIntegrationInfoToRequisitesDto> GetDataToMobileRequisitesAsync(int firmId, int userId);
}