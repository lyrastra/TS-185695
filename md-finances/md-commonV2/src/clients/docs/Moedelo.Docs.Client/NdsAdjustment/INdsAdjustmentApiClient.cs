using System.Threading.Tasks;
using Moedelo.Docs.Dto.NdsAdjustment;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.NdsAdjustment
{
    public interface INdsAdjustmentApiClient : IDI
    {
        /// <summary>
        /// Возвращает данные для начисления НДС на авансовые платежи
        /// </summary>
        Task<NdsAccrualCriteriaResponseDto> GetNdsAccrualByCriteriaAsync(int firmId, int userId, NdsAccrualCriteriaRequestDto requestDto);
        
        /// <summary>
        /// Возвращает данные для вычета НДС
        /// </summary>
        Task<NdsDeductionCriteriaResponseDto> GetNdsDeductionByCriteriaAsync(int firmId, int userId, NdsDeductionCriteriaRequestDto requestDto);
    }
}