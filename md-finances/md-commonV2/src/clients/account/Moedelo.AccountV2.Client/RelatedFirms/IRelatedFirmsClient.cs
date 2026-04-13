using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.RelatedFirms;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.RelatedFirms
{
    /// <summary>
    /// Клиент для работы с основными и дополнительными фирмами
    /// Дополнительная фирма - фирма, главный пользователь которой привязан к личному аккаунту главного пользователя главной фирмы
    /// </summary>
    public interface IRelatedFirmsClient : IDI
    {
        /// <summary>
        /// Получить главную и дополнительные фирмы
        /// </summary>
        /// <param name="firmId"></param>
        /// <returns></returns>
        Task<RelatedFirmsInfoDto> GetAsync(int firmId);

        /// <summary>
        /// Получить главную и дополнительные фирмы для списка фирм
        /// </summary>
        /// <param name="firmIds"></param>
        /// <returns></returns>
        Task<List<MainFirmInfoDto>> GetMainFirmInfosAsync(IReadOnlyCollection<int> firmIds);
    }
}