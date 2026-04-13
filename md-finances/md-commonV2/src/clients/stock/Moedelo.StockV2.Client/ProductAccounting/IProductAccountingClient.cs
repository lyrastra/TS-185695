using System.Threading;
using Moedelo.Common.Enums.Enums.ProductAccounting;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.ProductAccounting
{
    public interface IProductAccountingClient
    {
        /// <summary>
        /// Возвращает уровень доступа к функционалу расширенного товароучёта
        /// </summary>
        Task<ProductAccountingAccessLevel> GetAccessLevelAsync(int firmId, int userId, CancellationToken cancellationToken = default);
    }
}