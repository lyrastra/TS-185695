using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    public interface IPatentNotifyClient
    {
        /// <summary>
        /// Получение патентов для события "Подать заявление на продление ПСН"
        /// </summary>
        /// <param name="firmId">Идентификатор фирмы</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="endDate">Дата окончания события</param>
        /// <returns></returns>
        Task<List<PatentDto>> GetPatentsForProlongationEventAsync(int firmId, int userId, DateTime endDate);
    }
}
