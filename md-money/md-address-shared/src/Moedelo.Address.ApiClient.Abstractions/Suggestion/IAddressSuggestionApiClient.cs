using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions.Suggestion.Dto;
using Moedelo.Address.Enums;

namespace Moedelo.Address.ApiClient.Abstractions.Suggestion
{
    public interface IAddressSuggestionApiClient
    {
        /// <summary>
        /// Получение адреса по GUID
        /// </summary>
        /// <param name="guid">GUID адресного объекта</param>
        /// <param name="divisionType">Тип территориального деления</param>
        Task<SuggestionAddressWithoutBuildingDto> GetByGuidAsync(Guid guid, DivisionType divisionType, CancellationToken ct = default);

        /// <summary>
        /// Получение адреса по GUID
        /// </summary>
        /// <param name="guid">GUID адресного объекта</param>
        /// <param name="divisionType">Тип территориального деления</param>
        /// <returns>все адресные объекты до уровня с переданным гуидом</returns>
        Task<SuggestionAddressDto> GetByHouseGuidAsync(Guid guid, DivisionType divisionType, CancellationToken ct = default);

        /// <summary>
        /// Получение адресов по строке
        /// </summary>
        /// <param name="type">деление</param>
        /// <param name="query">адрес в виде строки</param>
        /// <param name="count">ограничение на кол-во подсказок</param>
        Task<SuggestionAddressDto[]> GetByQueryAsync(DivisionType type, string query, int count, CancellationToken ct);
    }
}
