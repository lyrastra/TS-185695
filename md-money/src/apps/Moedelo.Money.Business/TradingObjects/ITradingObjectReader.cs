using System.Threading.Tasks;

namespace Moedelo.Money.Business.TradingObjects
{
    public interface ITradingObjectReader
    {
        Task<TradingObject> GetByIdAsync(int tradingObjectId);
    }
}
