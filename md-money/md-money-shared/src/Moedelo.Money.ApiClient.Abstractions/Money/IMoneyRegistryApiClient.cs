using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Money.ApiClient.Abstractions.Money
{
    public interface IMoneyRegistryApiClient
    {
        Task<RegistryResponseDto> GetAsync(RegistryQueryDto query, HttpQuerySetting setting = null);

        /// <summary>
        /// Возвращает объединение исходящих платежей по кассе и рассчетным счетам для Виждетов НДС и Налога на прибыль
        /// </summary>
        Task<List<RegistryOperationDto>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate);
    }
}