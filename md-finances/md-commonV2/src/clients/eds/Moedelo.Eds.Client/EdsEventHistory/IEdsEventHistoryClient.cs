using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Eds.Dto;
using Moedelo.Eds.Dto.EdsEventHistory;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Eds.Client.EdsEventHistory
{
    public interface IEdsEventHistoryClient : IDI
    {
        /// <summary>
        /// Проверяет был ли переход на стек
        /// </summary>
        /// <param name="firmId"></param>
        Task<bool> HasTransferFlagAsync(int firmId);

        /// <summary>
        /// Возвращает информацию о переносе подписи
        /// </summary>
        /// <param name="firmId"></param>
        Task<EdsTransferInfoDto> GetTransferInfoAsync(int firmId);
        /// <summary>
        /// Возвращает AbnGuid пользователя в астрале или пустую строку если не было переноса ЭП
        /// </summary>
        /// <param name="firmId"></param>
        /// <returns>Возврат в запакованном объекте, т.к. некорректно десереализуется guid</returns>
        Task<DataDto<string>> GetPreviousAbnGuidAsync(int firmId);
        /// <summary>
        /// Возвращает AbnGuid пользователя в астрале и другие идентификаторы фирмы
        /// </summary>
        /// <param name="guids"></param>
        Task<IReadOnlyList<FirmIdentificatorDto>> GetFirmIdentificatorsAfterTransferAsync(IReadOnlyList<string> guids);
    }
}