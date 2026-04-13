using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Eds.Dto.EdsExpireInformation;

namespace Moedelo.Eds.Client.EdsExpireInformation
{
    public interface IEdsExpireApiClient
    {
        /// <summary>
        /// Возвращает информацию по ЭЦП, которые истекают в течении 30 дней, уже истёкшие по всем фирмам на дату
        /// </summary>
        Task<IReadOnlyList<ExpirationOnDateDto>> GetExpireInformationAsync(DateTime onDate);

        /// <summary>
        /// Возвращает информацию по ЭЦП кокретной фирмы на дату. Истекает ли ЭЦП в течении 30 дней или уже истекла
        /// </summary>
        Task<ExpirationOnDateDto> GetExpireInformationByFirmIdAsync(int firmId, DateTime onDate);
    }
}