using System;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.LimitExcessData.Dto;

namespace Moedelo.Billing.Abstractions.LimitExcessData.Interfaces;

public interface ILimitExcessApiClient
{
    /// <summary>
    /// Метод необходим для использования в системе Бухобслуживания для получения последнего
    /// допустимого значения оборота за указанный период (по дате начала действия лимита).
    /// Система Бухосблуживание работает только с "плоскими" объектами, поэтому клиент из md-authorization-shared не подходит
    /// </summary>
    /// <param name="dto">Параметры запроса</param>
    /// <returns></returns>
    [Obsolete("Не использовать!!! Воспользуйтесь клиентом авторизации для получения лимитов!!!")]
    Task<LimitValueResponseDto> GetLastMoneyTurnoverLimitAsync(FirmPeriodRequestDto dto);

    /// <summary>
    /// Метод необходим для использования в системе Бухобслуживания для получения последнего
    /// допустимого значения количества сотрудников на обслуживании за указанный период  (по дате начала действия лимита).
    /// Система Бухосблуживание работает только с "плоскими" объектами, поэтому клиент из md-authorization-shared не подходит
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [Obsolete("Не использовать!!! Воспользуйтесь клиентом авторизации для получения лимитов!!!")]
    Task<LimitValueResponseDto> GetLastNumberEmployeesInServiceLimitAsync(FirmPeriodRequestDto dto);
}