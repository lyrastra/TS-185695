using System;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.YaPay;

namespace Moedelo.Billing.Abstractions.YaPay;

public interface IYaPayOrdersApiClient
{
    /// <summary>
    /// Создание заказа для YaPay (Яндекс Сплит)
    /// </summary>
    /// <param name="dto">Данные для создания заказа</param>
    /// <returns>Dto с ответом</returns>
    Task<YaPayOrderCreationResponseDto> CreateOrderByBillNumberAsync(
        YaPayOrderCreationRequestDto dto);

    /// <summary>
    /// Получение заказа по внутреннему идентификатору заказа
    /// </summary>
    /// <param name="orderGuid">Внутренний индентификатор заказа (Guid)</param>
    Task<YaPayOrderDto> GetOrderByGuidAsync(Guid orderGuid);
}