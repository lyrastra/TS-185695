using System.Threading.Tasks;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// https://github.com/moedelo/md-commonV2/blob/master/src/clients/kontragents/Moedelo.KontragentsV2.Client/Kontragents/IKontragentFromOfficeClient.cs
    /// </summary>
    public interface IKontragentFromOfficeClient
    {
        /// <summary>
        /// Возвращает НЕСОХРАНЕННОГО контрагента с предзаполненными по ИНН реквизитами 
        /// </summary>
        Task<KontragentDto> GetAsync(int firmId, int userId, string inn);
    }
}
