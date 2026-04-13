using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.RequisitesV2.Dto.CustomCommonEvents;

namespace Moedelo.RequisitesV2.Client.CustomCommonEvents
{
    public interface ICustomCommonEventApiClient
    {
        /// <summary>
        /// Возвращает кастомное общее событие по идентификатору
        /// </summary>
        Task<CustomCommonEventResponseDto> GetAsync(int id);

        /// <summary>
        /// Создаёт кастомное общее событие и связывает с указанными логинами
        /// </summary>
        Task<CustomCommonEventResponseDto> CreateAsync(int userId, CustomCommonEventSaveDto dto);

        /// <summary>
        /// Изменяет кастомное общее событие
        /// </summary>
        Task<CustomCommonEventResponseDto> UpdateAsync(int userId, int id, CustomCommonEventSaveDto dto);

        /// <summary>
        /// Удаляет кастомное общее событие и разрывает связи с фирмами
        /// </summary>
        Task DeleteAsync(int id);

        /// <summary>
        /// Возвращает все кастомные общие события
        /// </summary>
        Task<IReadOnlyList<CustomCommonEventTableItemResponseDto>> GetAsync();
    }
}