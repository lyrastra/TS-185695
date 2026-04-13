using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.MeasureUnits;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.MeasureUnits
{
    public interface IMeasureUnitApiClient : IDI
    {
        /// <summary>
        /// Возвращает список единиц изменения (в т. ч. созданные пользователем), отсортированные по частоте использования
        /// </summary>
        Task<List<MeasureUnitAutocompleteItemDto>> AutocompleteAsync(int firmId, int userId, MeasureUnitsAutocompleteRequestDto request);
    }
}