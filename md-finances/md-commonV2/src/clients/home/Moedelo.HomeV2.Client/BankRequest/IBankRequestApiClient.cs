using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.BankPartners;
using Moedelo.HomeV2.Dto.BankRequest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.BankRequest
{
    public interface IBankRequestApiClient : IDI
    {
        /// <summary>
        /// отправка зявки в банк на расчётно-кассовое обслуживание (РКО)
        /// </summary>
        /// <returns></returns>
        Task<BankRequestResponseDto> SendOpenBankAccountRequestAsync(BankRequestDto requestDto);

        /// <summary>
        /// Получение списка отправленных заявок на расчётно-кассовое обслуживание
        /// </summary>
        /// <param name="bank">по какому банку-партнёру</param>
        /// <param name="startDate">Дата начала периода</param>
        /// <param name="endDate">Дата конца периода</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<SavedBankRequestDto>> GetRequestsByBankAsync(
            BankPartners bank, 
            string startDate,
            string endDate);
    }
}