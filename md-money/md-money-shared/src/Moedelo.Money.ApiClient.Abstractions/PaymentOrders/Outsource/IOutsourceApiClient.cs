using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource;

public interface IOutsourceApiClient
{
    /// <summary>
    /// Получшение данных о том, что обработаны все операции
    /// </summary>
    Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationApprovedAsync(
        OutsourceAllOperationsApprovedRequestDto request);

    /// <summary>
    /// Подтверждение операции из "Массовой работы с выписками"
    /// </summary>
    Task<OutsourceConfirmResultDto> ConfirmAsync<T>(T request) where T : class, IConfirmPaymentOrderDto, new();
        
    /// <summary>
    /// Удаление операции из "Массовой работы с выписками" 
    /// </summary>
    Task<OutsourceDeleteResultDto> DeleteAsync(long documentBaseId);
}