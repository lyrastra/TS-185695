using System.Threading.Tasks;
using Moedelo.Docs.Dto.PurchaseUpd;
using Moedelo.Docs.Dto.PurchaseUpd.Rest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.PurchasesUpd
{
    /// <summary>
    /// Клиент для использования external-api "Покупки - УПД" в private-режиме
    /// </summary>
    public interface IPurchasesUpdRestApiClient : IDI
    {
        /// <summary>
        /// Получить документ по DocumentBaseId (в терминах external это Id)
        /// </summary>
        Task<PurchaseUpdDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Создать НОВЫЙ документ
        /// </summary>
        Task<PurchaseUpdDto> CreateAsync(int firmId, int userId, PurchaseUpdSaveRequestDto dto);

        /// <summary>
        /// Удалить документ по DocumentBaseId (в терминах external это Id)
        /// </summary>
        Task DeleteAsync(int firmId, int userId, long documentBaseId);
    }
}