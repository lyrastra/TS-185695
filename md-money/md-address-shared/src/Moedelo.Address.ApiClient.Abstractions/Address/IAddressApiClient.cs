using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions.Address.Dto;

namespace Moedelo.Address.ApiClient.Abstractions.Address
{
    public interface IAddressApiClient
    {
        /// <summary>
        /// Получение адреса
        /// </summary>
        /// <param name="id">Индентификатор адреса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<AddressGetDto> GetAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение адресов
        /// </summary>
        /// <param name="ids">Индентификаторы адресов</param>
        Task<IReadOnlyDictionary<long, AddressGetDto>> GetAsync(IReadOnlyCollection<long> ids);

        /// <summary>
        /// Получение адреса в виде строки
        /// </summary>
        /// <param name="id">Индентификатор адреса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<string> GetStringAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение адреса в виде строки вместе с полем "Дополнительно"
        /// </summary>
        /// <param name="id">Индентификатор адреса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<string> GetStringWithAdditionalInfoAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Сохранение адреса
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы</param>
        /// <param name="dto">Адрес</param>
        /// <returns>Идентификатор адреса сохраненного адреса</returns>
        Task<long> CreateAsync(int firmId, AddressSaveDto dto);

        /// <summary>
        /// Сохранение списка адресов
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы</param>
        /// <param name="dto">Список адресов</param>
        /// <returns>Словарь пар (Id адреса, модель адреса)</returns>
        Task<IReadOnlyDictionary<long, AddressGetDto>> CreateListAsync(int firmId, IReadOnlyCollection<AddressSaveDto> dtos);

        /// <summary>
        /// Обновление адреса
        /// </summary>
        /// <param name="dto">Адрес</param>
        /// <param name="id">Идентификатор адреса</param>
        Task UpdateAsync(long id, AddressSaveDto dto);

        /// <summary>
        /// Удаление адреса
        /// </summary>
        /// <param name="id">Идентификатор адреса</param>
        /// <param name="firmId">Идентификатор фирмы</param>
        Task DeleteAsync(long id, int firmId);

        /// <summary>
        /// Удаление адресов списком
        /// </summary>
        /// <param name="addressList"> Список идентификаторов адреса</param>
        /// <param name="firmId">Идентификатор фирмы</param>
        Task DeleteListAsync(IReadOnlyCollection<long> addressList, int firmId);
    }
}
