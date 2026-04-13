using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OfficeV2.Dto.KontragentMultiCheck;

namespace Moedelo.OfficeV2.Client.KontragentMultiCheck
{
    public interface IKontragentMultiCheckApiClient : IDI
    {
        /// <summary>
        /// Проверка контрагентов
        /// <bold>Будьте внимательны! Изменился порядок параметров по сравнению с V1-клиентом</bold>
        /// </summary>
        Task<MultiCheckResponseDto> CheckAsync(int firmId, int userId, MultiCheckRequestDto request);

        /// <summary>
        /// Получение результатов проверки контрагента. Вызывать после метода CheckAsync до тех пор пока в результирующей моделе поле IsFinished не будет равно - 1
        /// </summary>
        Task<MultiCheckResponseDto> GetCheckResultAsync(int firmId, int userId, long requestId);

        /// <summary>
        /// Метод для стороннего API, не требует контекст пользователя.
        /// Всегда отдает полную модель данных.
        /// </summary>
        Task<MultiCheckResponseDto> CheckAsync(MultiCheckRequestDto request);
    }
}