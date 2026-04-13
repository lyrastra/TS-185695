using System;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.PaidConfigurations.Dto;

namespace Moedelo.Billing.Abstractions.PaidConfigurations.Interfaces;

public interface IPaidConfigurationsApiClient
{
    /// <summary>
    /// Метод для получения последней по дате начала действия оплаченной ПУ, продаваемой ГУ
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [Obsolete("Не использовать!!! Только для Бухобслуживания")]
    Task<PaidConfigurationResponseDto> GetLastPaidGlavUchetPaidProductConfigurationAsync(
        PaidGlavUchetConfigurationsRequestDto requestDto);
}