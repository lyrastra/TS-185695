using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.Abstractions
{
    public interface ICashOrderCreator
    {
        Task<CreateResponse> CreateAsync(CashOrderSaveRequest request);
    }
}