using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions
{
    public interface ICashOrderRemover
    {
        Task DeleteAsync(long documentBaseId);
    }
}