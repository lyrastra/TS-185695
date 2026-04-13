using System.Threading;
using System.Threading.Tasks;
using Moedelo.RequisitesV2.Dto.Stock;

namespace Moedelo.RequisitesV2.Client.Stock
{
    public interface IStockVisibilityClient
    {
        /// <summary>
        ///  Возвращает состояние складского учета
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="userId">идентификато пользователя фирмы</param>
        /// <param name="year">возвращает состояние склада на указанный год</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>видимо (true) или скрыт (false)</returns>
        Task<bool> IsVisibleAsync(int firmId, int userId, int? year = null, CancellationToken cancellationToken = default);
        Task<StockVisibilitySwitchResponseDto> SwitchOn(int firmId, int userId, bool switchOnWithOutValidation);
        Task<StockVisibilitySwitchResponseDto> SwitchOff(int firmId, int userId, bool switchOnWithOutValidation);
    }
}