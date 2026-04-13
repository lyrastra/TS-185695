using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Requisites;
using Moedelo.HomeV2.Client.Phone.Models;
using Moedelo.HomeV2.Dto.Phone;

namespace Moedelo.HomeV2.Client.Phone
{
    public interface IPhoneV2Client
    {
        Task<PhoneDto> Get(
            int firmId,
            int userId,
            PhoneTypes type,
            CancellationToken cancellationToken = default);

        Task<List<PhoneDto>> Get(
            IReadOnlyCollection<int> firmIds,
            IReadOnlyCollection<PhoneTypes> types,
            CancellationToken cancellationToken = default);

        Task<List<PhoneDto>> Get(
            string phone,
            PhoneTypes type,
            CancellationToken cancellationToken = default);

        Task UpdatePhone(
            int firmId,
            int userId,
            PhoneDto dto,
            CancellationToken cancellationToken = default);

        Task<bool> AvailableRegistrationForBankPartnerAsync(
            string phone,
            CancellationToken cancellationToken = default);

        Task<PhoneSearchByFilterResponseDto> SearchByFilterAsync(
            PhoneSearchByFilterRequestDto requestDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Создает фейковый номер телефона с кодом 948 и сохраняет в таблицу dbo.Phones (работает только на тестовых окружениях)
        /// </summary>
        /// <param name="requestDto">Тело запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданный телефон или null(если не удалось создать)</returns>
        Task<string> CreateFakePhoneAsync(
            FakePhoneCreateRequestDto requestDto,
            CancellationToken cancellationToken);
    }
}