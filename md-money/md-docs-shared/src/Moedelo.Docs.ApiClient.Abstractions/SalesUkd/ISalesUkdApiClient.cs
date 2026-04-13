using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUkd
{
    public interface ISalesUkdApiClient
    {
        /// <summary>
        /// Прочитать УКД по documentBaseId
        /// </summary>
        /// <param name="baseId"></param>
        /// <returns></returns>
        Task<Ukd> GetByBaseId(long baseId);

        /// <summary>
        /// Получить список УКД документов по операции из денег "Платеж на возврат"
        /// </summary>
        /// <param name="baseId"></param>
        /// <returns></returns>
        Task<List<Ukd>> GetByRefundPaymentBaseIdAsync(long baseId);
    }
}