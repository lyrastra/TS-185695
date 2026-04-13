using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Address.ApiClient.Abstractions.Address.Dto;

namespace Moedelo.Address.ApiClient.Abstractions.Address
{
    public interface IFirmAddressApiClient
    {
        /// <summary>
        /// Сохранение адреса фирмы
        /// </summary>
        /// <param name="firmId">Индентификатор фирмы</param>
        /// <param name="dto">Адрес</param>
        Task SaveAsync(int firmId, AddressSaveDto dto);

        /// <summary>
        /// Получение адреса фирмы
        /// </summary>
        /// <param name="firmId">Индентификатор фирмы</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<AddressGetDto> GetAsync(int firmId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение адресов фирм
        /// </summary>
        /// <param name="firmIds">Идентификатор фирм</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<IReadOnlyDictionary<int, AddressGetDto>> GetListAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken);

        /// <summary>
        /// Получение адреса фирмы в виде строки
        /// </summary>
        /// <param name="firmId">Индентификатор фирмы</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<string> GetStringAsync(int firmId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение адреса фирмы в виде строки с полем "Дополнительно"
        /// </summary>
        /// <param name="firmId">Индентификатор фирмы</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<string> GetStringWithAdditionalInfoAsync(int firmId, CancellationToken cancellationToken);

        /// <summary>
        /// Получение адресов фирм в виде строк
        /// </summary>
        /// <param name="firmIds">Индентификаторы фирм</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task<IReadOnlyDictionary<int, string>> GetStringsAsync(IReadOnlyCollection<int> firmIds, CancellationToken cancellationToken = default);
    }
}
