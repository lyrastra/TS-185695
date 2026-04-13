using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions
{
    public interface ICashOrderUpdater
    {
        Task UpdateAsync(CashOrderSaveRequest request);
    }
}