using System.Threading.Tasks;
using Moedelo.Docs.Dto.SalesUpd.Rest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.SalesUpd
{
    /// <summary>
    /// Клиент для использования external-api "Продажи - УПД" в private-режиме
    /// </summary>
    public interface ISalesUpdRestApiClient : IDI
    {
        /// <summary>
        /// Получить документ по DocumentBaseId (в терминах external это Id)
        /// </summary>
        Task<SalesUpdRestDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        /// <summary>
        /// Создать НОВЫЙ документ
        /// </summary>
        Task<SalesUpdRestDto> CreateAsync(int firmId, int userId, SalesUpdCreateRequestRestDto dto);

        /// <summary>
        /// Удалить документ по DocumentBaseId (в терминах external это Id)
        /// </summary>
        Task DeleteAsync(int firmId, int userId, long documentBaseId);
    }
}