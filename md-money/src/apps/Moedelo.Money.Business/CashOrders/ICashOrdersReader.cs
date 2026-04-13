using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders
{
    internal interface ICashOrdersReader
    {
        Task<CashOrder> GetByBaseIdAsync(long documentbaseId);
    }
}
