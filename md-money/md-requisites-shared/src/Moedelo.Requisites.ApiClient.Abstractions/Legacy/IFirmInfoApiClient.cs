using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IFirmInfoApiClient
    {
        Task<InvoiceSignerDto> GetInvoiceSignerAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Получение настроек автопривязки документов и платежей
        /// </summary>
        Task<AutoLinkSettingsDto> GetAutoLinkSettingsAsync(FirmId firmId, UserId userId);

        /// <summary>
        /// Установка настроек автопривязки документов и платежей
        /// </summary>
        Task SetAutoLinkSettingsAsync(FirmId firmId, UserId userId, AutoLinkSettingsDto settings);
    }
}
